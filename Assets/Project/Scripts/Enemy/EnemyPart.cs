using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Damage;
using Project.UI;
using UniRx;
using System;

namespace Project.Enemy
{
    [System.Serializable]
    public class EnemyPart : MonoBehaviour, IAttackable, IDamageable
    {
        private EnemyParameters _enemyParameters => transform.root.GetComponent<EnemyParameters>();
        private EnemyPartParameters _enemyPartParameters => _enemyParameters.QuestEnemyData.EnemyData.FindEnemyPartParameters(gameObject.name);

        //IAttackable
        public float ATK => _enemyPartParameters.ATK;

        public IReadOnlyReactiveProperty<bool> IsAttack => _enemyPartParameters.IsAttack;

        public List<Guid> HitGUID { get; set; } = new List<Guid>();

        public DamageObjectType ObjectTypeOpponent => DamageObjectType.Player;

        //IDamageable
        public float DEF => _enemyPartParameters.DEF;

        public Guid RootGUID => _enemyParameters.RootGUID;

        public DamageObjectType ObjectTypeMyself => DamageObjectType.Enemy;



        private GameObject DamageUI;

        private void Start()
        {
            DamageUI = GameObject.Find("GUI Plane");
        }

        public void AddDamage(int Damage)
        {
            var Parameters = transform.root.GetComponent<EnemyParameters>();
            Parameters.HP.Value -= Damage;
            DamageUI.GetComponent<UIEnemyDamageGenerator>().ShowDamage(transform.position, Damage);
            StartCoroutine(HitStop());
        }
        IEnumerator HitStop()
        {
            Time.timeScale = 0.3f;
            yield return new WaitForSecondsRealtime(0.3f);
            Time.timeScale = 1;
        }
    }
}