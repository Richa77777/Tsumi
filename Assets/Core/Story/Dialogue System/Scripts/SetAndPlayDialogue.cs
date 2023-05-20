using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class SetAndPlayDialogue : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private int _currentBranchIndex;

        public void PlayDialogue()
        {
            DialogueController.Instance.SetCurrentDialogue(_dialogue);
            _dialogue.A_SetBranchIndexAndPlay(_currentBranchIndex);
        }
    }
}
