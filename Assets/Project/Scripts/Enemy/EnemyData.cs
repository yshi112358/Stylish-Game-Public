using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Project.Enemy
{
    [CreateAssetMenu, System.Serializable]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<EnemyPartParameters> _enemyPartParameters;

        public string Name => _name;
        public int ID => _id;
        public GameObject Prefab => _prefab;
        public List<EnemyPartParameters> EnemyPartParameters => _enemyPartParameters;

        public EnemyPartParameters FindEnemyPartParameters(string boneName = null, string partName = null)
        {
            EnemyPartParameters parameter = new EnemyPartParameters();
            if (boneName != null)
                parameter = EnemyPartParameters.Find(_data => _data.BoneName == boneName);
            else if (partName != null)
                parameter = EnemyPartParameters.Find(_data => _data.PartName == partName);

            if (parameter != null)
                return parameter;
            else
                throw new System.Exception("FindEnemyPartParameters can't return EnemyPartParameter");
        }
    }
}