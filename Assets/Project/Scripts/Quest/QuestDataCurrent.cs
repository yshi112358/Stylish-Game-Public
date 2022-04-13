using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Quest
{
    [CreateAssetMenu, System.Serializable]
    public class QuestDataCurrent : ScriptableObject
    {
        public QuestData QuestData;
        public List<GameObject> EnemyList;
        public float Time;
        public Texture2D Texture2D;
    }
}