using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Enemy;

namespace Project.Quest
{
    [System.Serializable]
    public class QuestTarget
    {
        [SerializeField] private EnemyData _targetEnemy;
        [SerializeField] private int _number = 0;
        public EnemyData TargetEnemy => _targetEnemy;
        public int Number => _number;

        public void SetEnemy(EnemyData enemyData)
        {
            _targetEnemy = enemyData;
        }
        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}