using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Project.Enemy
{
    [System.Serializable]
    public class EnemyPartParameters
    {
        private float _atk;
        private readonly ReactiveProperty<bool> _isAttack = new ReactiveProperty<bool>(false);
        [SerializeField] private string _partName;
        [SerializeField] private float _def;
        [SerializeField] private string _boneName;

        public float ATK => _atk;
        public IReadOnlyReactiveProperty<bool> IsAttack => _isAttack;
        public float DEF => _def / 100;
        public string PartName => _partName;
        public string BoneName => _boneName;

        public void SetATK(float _atk)
        {
            this._atk = _atk;
        }
        public void SetIsAttack(bool isAttack)
        {
            _isAttack.Value = isAttack;
        }
    }
}