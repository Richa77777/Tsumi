using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DialogueSystem
{
    public class AnswerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Sprite _normalButtonSprite;
        [SerializeField] private Sprite _downButtonSprite;

        private Image _buttonImage;

        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonImage.sprite = _downButtonSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonImage.sprite = _normalButtonSprite;
        }
    }
}
