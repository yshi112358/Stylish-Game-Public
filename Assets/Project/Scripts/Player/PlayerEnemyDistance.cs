using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Quest;
using Project.Enemy;

namespace Project.Player
{
    public class PlayerEnemyDistance : MonoBehaviour
    {
        [SerializeField] private QuestDataCurrent _questDataCurrent;
        private List<GameObject> _nearEnemyList = new List<GameObject>();
        public List<GameObject> NearEnemyList => _nearEnemyList;
        void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _nearEnemyList.Clear();
                    foreach (var enemyObj in _questDataCurrent.EnemyList)
                        if (enemyObj.GetComponent<EnemyParameters>().HP.Value > 0)
                            _nearEnemyList.Add(enemyObj);
                    if (_nearEnemyList.Count > 0)
                        _nearEnemyList.Sort((a, b) => (int)(GetEnemyDistance(a) - GetEnemyDistance(b)));
                });
        }
        public float GetEnemyDistance(GameObject enemyObj)
        {
            return (enemyObj.transform.position - transform.position).magnitude;
        }
    }
}