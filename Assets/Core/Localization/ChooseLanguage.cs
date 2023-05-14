using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class ChooseLanguage : MonoBehaviour
    {
        [SerializeField] private Languages _language;

        public void SetLanguage()
        {
            LanguageController.Instance.SetCurrentLanguage(_language);
        }
    }
}
