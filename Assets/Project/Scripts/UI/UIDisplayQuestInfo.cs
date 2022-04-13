using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Quest;
using TMPro;
using UniRx;
using UniRx.Triggers;

namespace Project.UI
{
    public class UIDisplayQuestInfo : MonoBehaviour
    {
        [SerializeField] private QuestDataCurrent _questDataCurrent;
        private void Start()
        {
            var text = GetComponent<TextMeshProUGUI>();
            this.UpdateAsObservable()
                .Subscribe(_ => text.text = _questDataCurrent.QuestData.Info);
        }
    }
}