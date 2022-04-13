using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class EnemyAttackSerect : MonoBehaviour
    {
        void Start()
        {
            var animator = GetComponent<Animator>();
            int RandamNumber = Random.Range(1, 10);
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    float distance = GetComponent<EnemyPlayerDistance>().Enemyplayerdistance;
                    RandamNumber = Random.Range(1, 10);
                    if (distance < 1)
                    {
                        animator.SetTrigger("Attack");
                        if (RandamNumber <= 5) animator.SetInteger("Attacktype", 1);
                        else animator.SetInteger("Attacktype", 0);
                    }

                }
                );
        }
    }

}