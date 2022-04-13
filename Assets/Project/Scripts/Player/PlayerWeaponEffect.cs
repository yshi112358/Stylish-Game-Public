using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerWeaponEffect : MonoBehaviour
    {
        private PlayerWeaponEquip _weaponEquip;
        void Start()
        {
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            _weaponEquip = GetComponent<PlayerWeaponEquip>();
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => !x.StateInfo.IsTag("Attack"))
                .Subscribe(_ => EmitTrailOffCurrentWeapon())
                .AddTo(this);
        }

        private void EmitTrailOnCurrentWeapon()
        {
            var _currentWeaponTrail = _weaponEquip.WeaponParameters[_weaponEquip.WeaponCurrentIndex.Value].transform.GetChild(0).GetComponent<TrailRenderer>();
            _currentWeaponTrail.emitting = true;
        }
        private void EmitTrailOffCurrentWeapon()
        {
            var _currentWeaponTrail = _weaponEquip.WeaponParameters[_weaponEquip.WeaponCurrentIndex.Value].transform.GetChild(0).GetComponent<TrailRenderer>();
            _currentWeaponTrail.emitting = false;
        }
    }
}