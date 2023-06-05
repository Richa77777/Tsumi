using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    private Coroutine _setBarValueCor;

    public void SetBarValue(float value, float duration)
    {
        value = Mathf.Clamp(value, 0f, 1f);

        if (_setBarValueCor != null)
        {
            StopCoroutine(_setBarValueCor);
        }

        _setBarValueCor = StartCoroutine(SetBarValueCor(value, duration));
    }

    private IEnumerator SetBarValueCor(float value, float duration)
    {
        float currentBarValue = _barImage.fillAmount;

        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            _barImage.fillAmount = Mathf.Lerp(currentBarValue, value, time / duration);
            yield return null;
        }

        _barImage.fillAmount = value;
    }
}
