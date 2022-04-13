using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Project.Quest;

namespace Project.Enemy
{
    public class EnemyParameters : MonoBehaviour
    {
        private QuestEnemyData _questEnemyData;

        public ReactiveProperty<int> HP = new ReactiveProperty<int>();
        public readonly Guid RootGUID = Guid.NewGuid();
        public QuestEnemyData QuestEnemyData => _questEnemyData;

        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            HP.DistinctUntilChanged()
                .Where(x => x <= 0)
                .Take(1)
                .Subscribe(x => EnemyDeath())
                .AddTo(this);
        }
        private void EnemyDeath()//死んだときの処理
        {
            _animator.SetTrigger("Die");
        }
        public void SetEnemyData(QuestEnemyData enemyData)
        {
            _questEnemyData = enemyData;
            HP.Value = QuestEnemyData.HP;
        }
    }
}