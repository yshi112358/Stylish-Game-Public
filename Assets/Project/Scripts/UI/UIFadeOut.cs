using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class UIFadeOut : MonoBehaviour
    {
        private bool _isFadeEnd = false;
        public bool IsFadeEnd => _isFadeEnd;
        private void OnFadeOutExit()
        {
            _isFadeEnd = true;
        }
    }
}