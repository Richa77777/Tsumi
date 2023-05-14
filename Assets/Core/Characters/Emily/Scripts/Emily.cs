using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmilySpace
{
    public class Emily : MonoBehaviour
    {
        public static Emily Instance;

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
    }
}
