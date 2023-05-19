using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Localization;

namespace DialogueSystem
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private List<Branch> _branches = new List<Branch>();
        [SerializeField] private List<AnswerBlock> _answerBlocks = new List<AnswerBlock>();

        public List<Branch> BranchesGet => _branches;
        public List<AnswerBlock> AnswerBlocksGet => _answerBlocks;

        public void A_SetBranchIndexAndPlay(int index)
        {
            DialogueController.Instance.SetBranchIndexAndPlay(index);
        }

        public void A_EndDialogue()
        {
            DialogueController.Instance.EndDialogue();
        }

        public void A_EnableAnswers(int answerBlockIndex)
        {
            DialogueController.Instance.EnableAnswers(answerBlockIndex);
        }
    }

    #region Branches
    [System.Serializable]
    public class Branch
    {
        [SerializeField] private List<Phrase> _phrasesInBranch;

        public List<Phrase> PhrasesInBranchGet => _phrasesInBranch;
    }

    [System.Serializable]
    public class Phrase
    {
        [SerializeField] private Speaker _speaker;
        [SerializeField] private Sprite _sceneArt;
        [SerializeField] private string _textEn;
        [SerializeField] private string _textRu;
        [SerializeField] private UnityEvent _actionsAfterPhrase = new UnityEvent();

        public string TextGet(Languages language)
        {
            if (language == Languages.Ru)
            {
                return _textRu;
            }

            else if (language == Languages.En)
            {
                return _textEn;
            }

            return _textEn;
        }

        public Speaker SpeakerGet => _speaker;
        public Sprite SceneArtGet => _sceneArt;
        public UnityEvent ActionsAfterPhrase => _actionsAfterPhrase;
    }
    #endregion

    #region Answers
    [System.Serializable]
    public class AnswerBlock
    {
        [SerializeField] private List<Answer> _answers = new List<Answer>();

        public List<Answer> AnswersGet => _answers;
    }

    [System.Serializable]
    public class Answer
    {
        [SerializeField] private string _answerTextEn;
        [SerializeField] private string _answerTextRu;

        [SerializeField] private UnityEvent _answerActions = new UnityEvent();

        public string AnswerText(Languages language)
        {
            if (language == Languages.Ru)
            {
                return _answerTextRu;
            }

            else if (language == Languages.En)
            {
                return _answerTextEn;
            }

            return _answerTextEn;
        }

        public UnityEvent AnswerActions => _answerActions;
    }
    #endregion
}
