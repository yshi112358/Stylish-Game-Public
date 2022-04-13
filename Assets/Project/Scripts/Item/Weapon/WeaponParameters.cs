using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Damage;
using UniRx;
using UniRx.Triggers;
using System;

namespace Project.Item.Weapon
{
    public class WeaponParameters : MonoBehaviour, IAttackable
    {
        //非公開
        private readonly ReactiveProperty<bool> _isAttack = new ReactiveProperty<bool>();
        private float _motionValue;
        private WeaponData _weaponData;
        private int _index;

        //公開
        public float ATK => _weaponData.ATK * _motionValue;//武器の攻撃力*モーション値
        public IReadOnlyReactiveProperty<bool> IsAttack => _isAttack;
        public List<Guid> HitGUID { get; set; } = new List<Guid>();
        public DamageObjectType ObjectTypeOpponent => DamageObjectType.Enemy;
        public WeaponData WeaponData => _weaponData;
        public int Index => _index;

        public void SetIsAttack(bool isAttack)
        {
            _isAttack.Value = isAttack;
        }
        public void SetMotionValue(float motionValue)
        {
            _motionValue = motionValue;
        }
        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }
        public void SetIndex(int index)
        {
            _index = index;
        }
    }
}