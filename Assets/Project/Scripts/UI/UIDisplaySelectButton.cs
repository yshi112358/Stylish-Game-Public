using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Project.Manager;

namespace Project.UI
{
    public class UIDisplaySelectButton : MonoBehaviour
    {
        [SerializeField] private List<GameObject> SelectMethod = new List<GameObject>();
        private void Start()
        {
            InputMethodManager.CurrentControlDevice
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    foreach (var method in SelectMethod)
                        method.SetActive(false);
                    switch (x)
                    {
                        case InputMethodManager.ControlDeviceType.Gamepad:
                            SelectMethod[0].SetActive(true);
                            break;
                        case InputMethodManager.ControlDeviceType.KeyboardAndMouse:
                            SelectMethod[1].SetActive(true);
                            break;
                    }
                })
                .AddTo(this);
        }
    }
}