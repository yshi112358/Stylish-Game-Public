using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Quest
{
    [CreateAssetMenu, System.Serializable]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _iD;
        [SerializeField] private string _mapName;
        [SerializeField, Multiline] private string _info;
        [SerializeField] private Vector3 _playerPosition;
        [SerializeField] private List<QuestTarget> _questTarget;
        [SerializeField] private Sprite _image;
        [SerializeField] private List<QuestEnemyData> _questEnemyData;

        public string Name => _name;
        public int ID => _iD;
        public string MapName => _mapName;
        public string Info => _info;
        [SerializeField] public Vector3 PlayerPosition => _playerPosition;
        public List<QuestTarget> QuestTarget => _questTarget;
        public Sprite Image => _image;
        public List<QuestEnemyData> QuestEnemyData => _questEnemyData;
    }
}