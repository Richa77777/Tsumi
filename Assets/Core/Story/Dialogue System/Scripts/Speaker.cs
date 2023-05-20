using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Localization;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue System/Speaker")]
    public class Speaker : ScriptableObject
    {
        [SerializeField] private string _nameEn;
        [SerializeField] private string _nameRu;

        public string NameGet(Languages language)
        {
            if (language == Languages.Ru)
            {
                return _nameRu;
            }

            else if (language == Languages.En)
            {
                return _nameEn;
            }

            return "Name";
        }
    }
}