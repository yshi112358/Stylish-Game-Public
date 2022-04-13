using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Enemy
{
    [CreateAssetMenu, System.Serializable]
    public class EnemyList : ScriptableObject
    {
        [SerializeField]
        private List<EnemyData> _enemyData = new List<EnemyData>();
        public List<EnemyData> EnemyData => _enemyData;

        EnemyData FindID(int ID)
        {
            return EnemyData.Find(Data => Data.ID == ID);
        }
    }
}