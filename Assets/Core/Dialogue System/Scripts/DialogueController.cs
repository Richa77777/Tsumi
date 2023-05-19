using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Localization;

namespace DialogueSystem
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController Instance;

        [SerializeField] private GameObject _dialogueTab;
        [SerializeField] private Button[] _answerButtons = new Button[10];

        [SerializeField] private TextMeshProUGUI _speakerName;
        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private Image _artField;
        [SerializeField] private AudioClip _typewriterSound;

        [SerializeField] private float _timeBtwnChars = 0.05f;

        private int _currentBranchIndex = 0;
        private bool _skip = false;
        private Dialogue _currentDialogue;
        private AudioSource _audioSource;
        private IEnumerator _playCurrentBranchCor;
        private IEnumerator _writePhraseCor;

        public int CurrentBranchIndexGet => _currentBranchIndex;
        public Dialogue CurrentDialogue => _currentDialogue;


        private void Awake()
        {
            #region Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
            #endregion

            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _typewriterSound;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void SetCurrentDialogue(Dialogue dialogue)
        {
            _currentDialogue = dialogue;
        }

        public void SetBranchIndexAndPlay(int index)
        {
            _currentBranchIndex = index;

            PlayCurrentBranch();
        }

        private void PlayCurrentBranch()
        {
            if (_currentDialogue != null)
            {
                StopPlayCurrentBranch();

                _playCurrentBranchCor = PlayCurrentBranchCor();
                StartCoroutine(_playCurrentBranchCor);
            }
        }

        private void StopPlayCurrentBranch()
        {
            if (_playCurrentBranchCor != null) StopCoroutine(_playCurrentBranchCor);

            if (_writePhraseCor != null)
            {
                StopCoroutine(_writePhraseCor);
                _writePhraseCor = null;
            }

            _speakerText.text = "";
            _speakerName.text = "";
        }

        public void EndDialogue()
        {
            if (_currentDialogue != null)
            {
                _skip = false;

                StopPlayCurrentBranch();

                _dialogueTab.SetActive(false);
                _currentDialogue = null;
            }
        }

        #region Answers
        public void EnableAnswers(int answerBlockIndex)
        {
            if (_currentDialogue != null)
            {
                StopPlayCurrentBranch();

                AnswerBlock currentAnswerBlock = _currentDialogue.AnswerBlocksGet[answerBlockIndex];

                for (int i = 0; i < _currentDialogue.AnswerBlocksGet[answerBlockIndex].AnswersGet.Count; i++)
                {
                    for (int j = 0; j < _answerButtons.Length; j++)
                    {
                        if (_answerButtons[j].gameObject.activeInHierarchy == false)
                        {
                            _answerButtons[j].onClick.AddListener(currentAnswerBlock.AnswersGet[i].AnswerActions.Invoke);
                            _answerButtons[j].onClick.AddListener(DisableAnswers);

                            _answerButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = currentAnswerBlock.AnswersGet[i].AnswerText(LanguageController.Instance.CurrentLanguageGet);
                            _answerButtons[j].gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }
        public void DisableAnswers()
        {
            if (_currentDialogue != null)
            {
                for (int i = 0; i < _answerButtons.Length; i++)
                {
                    if (_answerButtons[i].gameObject.activeInHierarchy == true)
                    {
                        _answerButtons[i].onClick = new Button.ButtonClickedEvent();
                        _answerButtons[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        #endregion

        private IEnumerator PlayCurrentBranchCor()
        {
            yield return new WaitForSeconds(0.05f);

            _skip = false;

            _dialogueTab.SetActive(true);

            Branch currentBranch = _currentDialogue.BranchesGet[_currentBranchIndex];
            Phrase currentPhrase = null;

            for (int currentPhraseIndex = 0; currentPhraseIndex < currentBranch.PhrasesInBranchGet.Count; currentPhraseIndex++)
            {
                _skip = false;

                currentPhrase = currentBranch.PhrasesInBranchGet[currentPhraseIndex];
                _speakerName.text = currentPhrase.SpeakerGet.NameGet(LanguageController.Instance.CurrentLanguageGet);

                if (currentPhrase.SceneArtGet != null)
                {
                    _artField.sprite = currentPhrase.SceneArtGet;
                }

                Languages currentLanguage = LanguageController.Instance.CurrentLanguageGet;

                if (_writePhraseCor != null) StopCoroutine(_writePhraseCor);
                _writePhraseCor = WritePhraseCor(currentPhrase.TextGet(LanguageController.Instance.CurrentLanguageGet));

                StartCoroutine(CheckPressed());

                yield return StartCoroutine(_writePhraseCor);
                yield return new WaitForSeconds(0.05f);
                yield return new WaitUntil(SkipButtonPressed);

                currentPhrase.ActionsAfterPhrase?.Invoke();
            }

            _writePhraseCor = null;
            _playCurrentBranchCor = null;
        }

        private IEnumerator WritePhraseCor(string text)
        {
            for (int i = 0; i <= text.Length && _skip == false; i++)
            {
                _speakerText.text = text.Substring(0, i);
                _audioSource.Play();

                if (i > 0)
                {
                    string lastChar = text.ToCharArray()[i - 1].ToString();

                    if (lastChar == "," || lastChar == "." || lastChar == "!" || lastChar == "?")
                    {
                        yield return new WaitForSeconds(_timeBtwnChars * 5);
                        continue;
                    }
                }

                yield return new WaitForSeconds(_timeBtwnChars);
            }

            _speakerText.text = text;
        }

        private IEnumerator CheckPressed()
        {
            yield return new WaitForSeconds(0.05f);
            yield return new WaitUntil(SkipButtonPressed);

            _skip = true;
        }

        public bool SkipButtonPressed()
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.E))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    return true;
                }
            }

            return false;
        }

    }
}
