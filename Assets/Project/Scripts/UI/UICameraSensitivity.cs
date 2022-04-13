using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Project.UI
{
    public class UICameraSensitivity : MonoBehaviour
    {
        [SerializeField] private PlayerCameraSensitivityData data;
        void Start()
        {
            var slider = GetComponent<Slider>();
            slider.value = data.Sensitivity;
            this.UpdateAsObservable()
                .Subscribe(_ => data.Sensitivity = slider.value);
        }
    }
}