using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Project.Manager
{
    public class QuitManger : MonoBehaviour
    {
        [SerializeField] private GameObject _exitWindow;
        void Start()
        {
            var inputActions = new PlayerInputActions();
            GameObject exitWindow = null;
            inputActions.UI.Enable();
            this.UpdateAsObservable()
                .Where(_ => !exitWindow)
                .Where(_ => inputActions.UI.Config.triggered)
                .Subscribe(_ =>
                {
                    exitWindow = Instantiate(_exitWindow);
                    Time.timeScale = 0;
                });
        }
    }
}