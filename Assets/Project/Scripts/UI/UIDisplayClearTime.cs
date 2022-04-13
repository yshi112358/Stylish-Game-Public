using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Quest;
using System;

namespace Project.UI
{
    public class UIDisplayClearTime : MonoBehaviour
    {
        private Text _text => GetComponent<Text>();
        [SerializeField] QuestDataCurrent _questDataCurrent;
        void Start()
        {
            var sec =
            _text.text = new TimeSpan(0, 0, 0, 0, (int)(_questDataCurrent.Time * 1000)).ToString(@"mm\'\ ss'"" 'ff");
        }
    }
}