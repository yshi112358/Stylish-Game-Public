using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
namespace Project.UI
{
    public class UIResetSelect : MonoBehaviour
    {
        void Start()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            this.UpdateAsObservable()
                .ObserveEveryValueChanged(_ => gameObject.activeSelf)
                .Where(x => x)
                .Subscribe(_ => EventSystem.current.SetSelectedGameObject(gameObject));
        }
    }
}