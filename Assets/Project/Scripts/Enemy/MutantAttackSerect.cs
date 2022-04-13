using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class MutantAttackSerect : MonoBehaviour
    {
        void Start()
        {
            var animator = GetComponent<Animator>();
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    float distance = GetComponent<EnemyPlayerDistance>().Enemyplayerdistance;
                    if (3 < distance && distance < 5)
                    {
                        animator.SetInteger("Attacktype",1);

                    }
                    else
                    {
                        animator.SetInteger("Attacktype",0);
                    }
                }
                );
        }
    }

}