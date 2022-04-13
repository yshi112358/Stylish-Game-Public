using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Item.Weapon;

namespace Project.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerWeaponEquip _weaponEquip;
        private int _weaponCurrentIndex => _weaponEquip.WeaponCurrentIndex.Value;
        private WeaponParameters[] _weaponParameters => _weaponEquip.WeaponParameters;

        private void Awake()
        {
            _weaponEquip = GetComponent<PlayerWeaponEquip>();
        }

        private void Start()
        {
            var input = GetComponent<IInputEventProvider>();
            var animator = GetComponent<Animator>();

            input.AttackButtonX
                .DistinctUntilChanged()
                .Where(x => x)
                .Subscribe(_ => 
                {
                    animator.SetInteger("AttackType",0);
                    animator.SetTrigger("Attack");
                })
                .AddTo(this);

            input.AttackButtonY
                .DistinctUntilChanged()
                .Where(x => x)
                .Subscribe(_ => 
                {
                    animator.SetInteger("AttackType",1);
                    animator.SetTrigger("Attack");
                })
                .AddTo(this);
        }

        private void OnAttackEnter(float motionValue)
        {
            _weaponParameters[_weaponCurrentIndex].SetIsAttack(true);
            _weaponParameters[_weaponCurrentIndex].SetMotionValue(motionValue);
        }
        private void OnAttackExit()
        {
            _weaponParameters[_weaponCurrentIndex].SetIsAttack(false);
        }
    }
}