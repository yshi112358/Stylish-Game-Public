using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Project.Player;

public class UIPlayerHP : MonoBehaviour
{
    [SerializeField] PlayerParameters _playerParameters;
    void Start()
    {
        var slider = GetComponent<Slider>();
        _playerParameters.HP
            .DistinctUntilChanged()
            .Subscribe(x => slider.value = (float)x / (float)_playerParameters.MaxHP.Value)
            .AddTo(this);
    }
}
