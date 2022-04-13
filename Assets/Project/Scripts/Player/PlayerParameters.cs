using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Project.Damage;
using Project.UI;
using Project.Manager;

namespace Project.Player
{
    public class PlayerParameters : MonoBehaviour, IDamageable
    {
        private readonly ReactiveProperty<int> _hp = new ReactiveProperty<int>(100);
        private readonly ReactiveProperty<int> _maxHP = new ReactiveProperty<int>(100);
        private float _def = 600;
        private readonly Guid _rootGUID = Guid.NewGuid();
        private readonly DamageObjectType _objectTypeMyself = DamageObjectType.Player;

        private bool _invincible => GetComponent<PlayerEscape>().Invincible;
        [SerializeField] Animator _uiGameOver;
        [SerializeField] QuestManager _questManager;
        public IReadOnlyReactiveProperty<int> HP => _hp;
        public IReadOnlyReactiveProperty<int> MaxHP => _maxHP;
        public float ATK;
        public float DEF => _def / 1000;
        public Guid RootGUID => _rootGUID;
        public DamageObjectType ObjectTypeMyself => _objectTypeMyself;

        public GameObject CurrentWeapon;

        void Start()
        {
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            _hp.DistinctUntilChanged()
                .Subscribe(x => animator.SetFloat("HP", x))
                .AddTo(this);
            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Subscribe(l => _hp.Value += 1)
                .AddTo(this);
            _hp.DistinctUntilChanged()
                .Where(x => x < 0)
                .Take(1)
                .Subscribe(_ => animator.SetTrigger("Death"))
                .AddTo(this);
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("Death"))
                .Subscribe(_ => _uiGameOver.SetTrigger("Death"))
                .AddTo(this);
        }
        public void AddDamage(int Damage)
        {
            if (!_invincible && !_questManager.QuestClear)
                _hp.Value -= Damage;
        }
    }
}