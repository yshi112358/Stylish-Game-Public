using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class EnemyLocomotion : MonoBehaviour
    {
        NavMeshHit navMeshHit;
        [SerializeField] float wanderRange;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Transform target;
        void Start()
        {
            var navmeshagent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            //追尾
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x=>x.StateInfo.IsName("Chase"))
                .Subscribe(_=>agent.SetDestination(target.position))
                .AddTo(this);
            //徘徊
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Wander"))
                .Subscribe(_ => RandomWander())
                .AddTo(this);
            //間合い
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x =>x.StateInfo.IsName("Spacing"))
                .Subscribe(_=>
                {
                    agent.SetDestination(transform.position);
                    animator.SetTrigger("Attack");
                }
                )
                .AddTo(this);
            //攻撃
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Attack"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(target.position);
                })
                .AddTo(this);
        }
        void RandomWander()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        SetDestination();
                    }
                }
            }
        }
        void SetDestination()
        {
            Vector3 randomPos = new Vector3(Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange));
            NavMesh.SamplePosition(randomPos, out navMeshHit, 100, 1);
            agent.destination = navMeshHit.position;
        }
    }
}