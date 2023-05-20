using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class ChooseLanguage : MonoBehaviour
    {
        public void SetLanguage(Languages language)
        {
            LanguageController.Instance.SetCurrentLanguage(language);
        }
    }
}
