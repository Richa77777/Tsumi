using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fade
{
    public class FadeController : MonoBehaviour
    {
        public static FadeController Instance;

        [SerializeField] private CanvasGroup _fade;
        [SerializeField] private float _fadeTime;

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

        public void CallFade(bool isOut)
        {
            StartCoroutine(CallFadeCor(isOut));
        }

        private IEnumerator CallFadeCor(bool isOut)
        {
            float targetAlpha = isOut == true ? 1f : 0f;
            float currentAlpha = isOut == true ? 0f : 1f;

            for (float t = 0; t < _fadeTime; t += Time.deltaTime)
            {
                _fade.alpha = Mathf.Lerp(currentAlpha, targetAlpha, t / _fadeTime);
                yield return null;
            }

            _fade.alpha = targetAlpha;
        }
    }
}
