using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingBar : MonoBehaviour
{
    private Image _image;
    private Coroutine _setBarValueCor;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

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
        float currentBarValue = _image.fillAmount;
        
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            _image.fillAmount = Mathf.Lerp(currentBarValue, value, time / duration);
            yield return null;
        }

        _image.fillAmount = value;
    }
}

