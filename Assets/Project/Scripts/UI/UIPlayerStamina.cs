using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Project.Player;

namespace Project.UI
{
    public class UIPlayerStamina : MonoBehaviour
    {
        [SerializeField] private PlayerStamina stamina;
        void Start()
        {
            var slider = GetComponent<Slider>();
            stamina.ST
                .DistinctUntilChanged()
                .Subscribe(x => slider.value = x / stamina.MaxST.Value)
                .AddTo(this);
        }
    }
}