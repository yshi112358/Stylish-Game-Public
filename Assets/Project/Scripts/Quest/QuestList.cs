using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Quest
{
    [CreateAssetMenu, System.Serializable]
    public class QuestList : ScriptableObject
    {
        [SerializeField] private List<QuestData> _questData = new List<QuestData>();
        public List<QuestData> QuestData => _questData;
    }
}