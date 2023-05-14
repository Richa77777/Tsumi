using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizableText : MonoBehaviour
    {
        [SerializeField] private string _enText;
        [SerializeField] private string _ruText;

        private TextMeshProUGUI _textMeshPro;
        private Coroutine _waitForLanguageControllerCor;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _waitForLanguageControllerCor = StartCoroutine(WaitForLanguageController());
        }

        private void OnEnable()
        {
            LanguageController.Instance.LanguageChanged += SetText;
            SetText(LanguageController.Instance.CurrentLanguageGet);
        }

        private void OnDisable()
        {
            if (_waitForLanguageControllerCor != null)
            {
                StopCoroutine(_waitForLanguageControllerCor);
            }

            if (LanguageController.Instance != null)
            {
                LanguageController.Instance.LanguageChanged -= SetText;
            }
        }

        public void SetText(Languages language)
        {
            _textMeshPro.text = language == Languages.En ? _enText : _ruText;
        }

        private IEnumerator WaitForLanguageController()
        {
            while (LanguageController.Instance == null)
            {
                yield return null;
            }

            LanguageController.Instance.LanguageChanged += SetText;

            SetText(LanguageController.Instance.CurrentLanguageGet);
        }
    }
}