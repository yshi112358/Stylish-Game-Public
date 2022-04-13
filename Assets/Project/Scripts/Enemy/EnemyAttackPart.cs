using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Enemy
{
    [CreateAssetMenu, System.Serializable]
    public class EnemyAttackPart : ScriptableObject
    {
        [SerializeField] private string _partName;
        [SerializeField] private float _atk;

        public string PartName => _partName;
        public float ATK => _atk;
    }
}