using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fade
{
    public class FadeCall : MonoBehaviour
    {
        public void CallFade(bool isOut)
        {
            FadeController.Instance.CallFade(isOut);
        }
    }
}