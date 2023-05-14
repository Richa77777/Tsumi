using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class LanguageController : MonoBehaviour
    {
        public static LanguageController Instance;

        [SerializeField] private Languages _currentLanguage = Languages.En;

        public Action<Languages> LanguageChanged;
        public Languages CurrentLanguageGet => _currentLanguage;

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
        }

        public void SetCurrentLanguage(Languages language)
        {
            _currentLanguage = language;
            LanguageChanged?.Invoke(language);
        }
    }

    public enum Languages
    {
        En,
        Ru
    }
}
