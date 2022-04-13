using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Project.Quest;

public class UIDisplayEnemyImage : MonoBehaviour
{
    [SerializeField] private QuestDataCurrent _questDataCurrent;
    void Start()
    {
        var image = GetComponent<Image>();
        this.UpdateAsObservable()
            .Subscribe(_ => image.sprite = _questDataCurrent.QuestData.Image);
    }
}
