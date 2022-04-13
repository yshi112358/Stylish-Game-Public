using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Quest;
using UniRx;
using UniRx.Triggers;
using TMPro;

namespace Project.UI
{
    public class UIDisplayCurrentQuestName : MonoBehaviour
    {
        private TextMeshProUGUI _text => GetComponent<TextMeshProUGUI>();
        [SerializeField] QuestDataCurrent _questDataCurrent;
        private void Start()
        {
            DisplayCurrentQuestName();
        }
        public void DisplayCurrentQuestName()
        {
            _text.text = _questDataCurrent.QuestData.Name;
        }
    }
}
