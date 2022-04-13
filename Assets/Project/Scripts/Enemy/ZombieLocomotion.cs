using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class ZombieLocomotion : MonoBehaviour
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
            target = GameObject.Find("Player").transform;
            //追尾
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Chase"))
                .Subscribe(_ =>
                {
                    agent.speed = 4f;
                    agent.SetDestination(target.position);
                })
                .AddTo(this);
            //徘徊
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Wander"))
                .Subscribe(_ =>
                {
                    agent.speed = 0.5f;
                    RandomWander();
                })
                .AddTo(this);
            //間合い
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Spacing"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(transform.position);
                    animator.SetTrigger("Attack");
                }
                )
                .AddTo(this);
            //攻撃
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Attack1"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(agent.transform.position);
                    transform.LookAt(target);
                })
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Attack2"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(agent.transform.position);
                    transform.LookAt(target);
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