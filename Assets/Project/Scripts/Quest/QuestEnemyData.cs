using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Enemy;

namespace Project.Quest
{
    [System.Serializable]
    public class QuestEnemyData
    {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private int _hp;
        [SerializeField] private List<Vector3> _spawnPos;


        public EnemyData EnemyData => _enemyData;
        public int HP => _hp;
        public List<Vector3> SpawnPos => _spawnPos;
    }
}