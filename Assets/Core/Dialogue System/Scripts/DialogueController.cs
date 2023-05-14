using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        [SerializeField] private TextMeshProUGUI _speakerStateText;
        [SerializeField] private Image _speakerPortrait;
        [SerializeField] private AudioClip _typewriterSound;

        [SerializeField] private float _timeBtwnChars = 0.05f;

        private int _currentBranchIndex = 0;
        private Dialogue _currentDialogue;
        private IEnumerator _playCurrentBranchCor;
        private IEnumerator _writePhraseCor;
        private bool _skip = false;
        private AudioSource _audioSource;

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
                _speakerStateText.text = System.Enum.GetName(typeof(PortraitStates), currentPhrase.SpeakerStateGet);

                Languages currentLanguage = LanguageController.Instance.CurrentLanguageGet;

                switch (currentPhrase.SpeakerStateGet)
                {
                    case PortraitStates.Neutral:
                        _speakerStateText.text = currentLanguage == Languages.En ? "Neutral" : "Нейтральный";
                        //_speakerPortrait.sprite = currentPhrase.SpeakerGet.PortraitsLibraryGet.GetSprite("Portraits", "Neutral");
                        break;

                    case PortraitStates.Happy:
                        _speakerStateText.text = currentLanguage == Languages.En ? "Happy" : "Счастливый";
                        //_speakerPortrait.sprite = currentPhrase.SpeakerGet.PortraitsLibraryGet.GetSprite("Portraits", "Happy");
                        break;

                    case PortraitStates.Sad:
                        _speakerStateText.text = currentLanguage == Languages.En ? "Sad" : "Грустный";
                        //_speakerPortrait.sprite = currentPhrase.SpeakerGet.PortraitsLibraryGet.GetSprite("Portraits", "Sad");
                        break;

                    case PortraitStates.Angry:
                        _speakerStateText.text = currentLanguage == Languages.En ? "Angry" : "Сердитый";
                        //_speakerPortrait.sprite = currentPhrase.SpeakerGet.PortraitsLibraryGet.GetSprite("Portraits", "Angry");
                        break;

                    case PortraitStates.Puzzled:
                        _speakerStateText.text = currentLanguage == Languages.En ? "Puzzled" : "Озадаченный";
                        //_speakerPortrait.sprite = currentPhrase.SpeakerGet.PortraitsLibraryGet.GetSprite("Portraits", "Puzzled");
                        break;
                }

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

                    if (lastChar == ",")
                    {
                        yield return new WaitForSeconds(_timeBtwnChars * 3);
                        continue;
                    }

                    else if (lastChar == "." || lastChar == "!" || lastChar == "?")
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
                return true;
            }

            return false;
        }

    }
}
