using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Item.Weapon;
using System;
using UnityEngine.Animations.Rigging;

namespace Project.Player
{
    public class PlayerWeaponEquip : MonoBehaviour
    {
        //非公開
        private int[] _weaponEquippedList = { 0, 1, 0, 1 };
        private readonly ReactiveProperty<int> _weaponCurrentIndex = new ReactiveProperty<int>(0);
        private WeaponParameters[] _weaponParameters = new WeaponParameters[4];
        private Animator[] _weaponAnimator = new Animator[4];
        [SerializeField] private WeaponList _weaponList;
        [SerializeField] private GameObject[] _weaponGrip = new GameObject[4];
        //公開
        public int[] WeaponEquippedList => _weaponEquippedList;
        public IReadOnlyReactiveProperty<int> WeaponCurrentIndex => _weaponCurrentIndex;
        public WeaponParameters[] WeaponParameters => _weaponParameters;

        void Start()
        {
            var input = GetComponent<IInputEventProvider>();
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            //Time.timeScale = 0.4f;
            //武器生成
            for (int n = 0; n < 4; n++)
            {
                var WeaponGeneratedData = _weaponList.WeaponData.Find(Data => Data.ID == WeaponEquippedList[n]);
                var WeaponGemeratedObj = Instantiate(WeaponGeneratedData.Prefab, _weaponGrip[n].transform);
                WeaponGemeratedObj.transform.localPosition = Vector3.zero;
                WeaponGemeratedObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
                _weaponParameters[n] = WeaponGemeratedObj.GetComponent<WeaponParameters>();
                _weaponParameters[n].SetWeaponData(WeaponGeneratedData);
                _weaponParameters[n].SetIndex(n);
                _weaponAnimator[n] = _weaponGrip[n].GetComponent<Animator>();
            }
            //初期武器
            SetCurrentWeaponRightLeftHand();
            //武器変更
            var inputIndex = 0;
            input.WeaponButton
                .ObserveReplace()
                .DistinctUntilChanged()
                .Where(x => x.NewValue)//新しい値がtrueの時
                .Where(x => x.Index != _weaponCurrentIndex.Value)
                .Subscribe(x =>
                {
                    animator.SetTrigger("WeaponChange");
                    inputIndex = x.Index;
                })
                .AddTo(this);
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("WeaponChange"))
                .Subscribe(_ =>
                {
                    _weaponCurrentIndex.Value = inputIndex;
                })
                .AddTo(this);
            //BrendTreeの場合AnimationEventが使えない為
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsTag("Locomotion"))
                .Subscribe(_ => SetCurrentWeaponRightLeftHand())
                .AddTo(this);
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsTag("Climb"))
                .Subscribe(_ => SetAllWeaponFloat())
                .AddTo(this);

        }
        private void SetAllWeaponFloat()
        {
            for (int n = 0; n < 4; n++)
                SetWeaponFloat(n);
        }
        private void SetCurrentWeaponRightLeftHand()
        {
            SetAllWeaponFloat();
            SetRightLeftHand(_weaponCurrentIndex.Value);
        }
        private void SetCurrentWeaponLeftRightHand()
        {
            SetAllWeaponFloat();
            SetLeftRightHand(_weaponCurrentIndex.Value);
        }
        private void SetCurrentWeaponRightHand()
        {
            SetAllWeaponFloat();
            SetRightHand(_weaponCurrentIndex.Value);
        }
        private void SetCurrentWeaponLeftHand()
        {
            SetAllWeaponFloat();
            SetLeftHand(_weaponCurrentIndex.Value);
        }
        private void SetWeaponFloat(int n)
        {
            _weaponAnimator[n].SetBool("IsRightLeftHand", false);
            _weaponAnimator[n].SetBool("IsLeftRightHand", false);
            _weaponAnimator[n].SetBool("IsRightHand", false);
            _weaponAnimator[n].SetBool("IsLeftHand", false);
            _weaponAnimator[n].SetBool("IsFloat", true); //Debug.Log("Float");
        }
        private void SetRightLeftHand(int n)
        {
            _weaponAnimator[n].SetBool("IsRightLeftHand", true); Debug.Log("RightLeftHand");
            _weaponAnimator[n].SetBool("IsLeftRightHand", false);
            _weaponAnimator[n].SetBool("IsRightHand", false);
            _weaponAnimator[n].SetBool("IsLeftHand", false);
            _weaponAnimator[n].SetBool("IsFloat", false);
        }
        private void SetLeftRightHand(int n)
        {
            _weaponAnimator[n].SetBool("IsRightLeftHand", false);
            _weaponAnimator[n].SetBool("IsLeftRightHand", true); Debug.Log("LeftRightHand");
            _weaponAnimator[n].SetBool("IsRightHand", false);
            _weaponAnimator[n].SetBool("IsLeftHand", false);
            _weaponAnimator[n].SetBool("IsFloat", false);
        }
        private void SetRightHand(int n)
        {
            _weaponAnimator[n].SetBool("IsRightLeftHand", false);
            _weaponAnimator[n].SetBool("IsLeftRightHand", false);
            _weaponAnimator[n].SetBool("IsRightHand", true); Debug.Log("RightHand");
            _weaponAnimator[n].SetBool("IsLeftHand", false);
            _weaponAnimator[n].SetBool("IsFloat", false);
        }
        private void SetLeftHand(int n)
        {
            _weaponAnimator[n].SetBool("IsRightLeftHand", false);
            _weaponAnimator[n].SetBool("IsLeftRightHand", false);
            _weaponAnimator[n].SetBool("IsRightHand", false);
            _weaponAnimator[n].SetBool("IsLeftHand", true); Debug.Log("LeftHand");
            _weaponAnimator[n].SetBool("IsFloat", false);
        }
    }
}