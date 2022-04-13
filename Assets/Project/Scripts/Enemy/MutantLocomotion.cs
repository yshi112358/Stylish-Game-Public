using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class MutantLocomotion : MonoBehaviour
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
            Vector3 latestpos = transform.position;
            //追尾
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Chase"))
                .Subscribe(_ =>
                {
                    agent.speed = 5f;
                    agent.SetDestination(target.position);
                })
                .AddTo(this);
            //徘徊
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Wander"))
                .Subscribe(_ =>
                {
                    agent.speed = 3f;
                    RandomWander();
                })
                .AddTo(this);
            //間合い
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Idle"))
                .Subscribe(_ =>
                {
                    agent.speed = 4f;
                    Vector3 posDiff = transform.position - latestpos;
                    latestpos = transform.position;
                    if (posDiff.magnitude > 0.01f)
                    {
                        animator.SetBool("isMove", true);
                    }
                    else
                    {
                        animator.SetBool("isMove", false);
                    }
                    var directionTarget = (target.position - agent.transform.position).normalized;
                    var targetPosition = target.position - directionTarget * 5;
                    //Debug.Log(targetPosition);
                    agent.SetDestination(targetPosition);
                    float directionDiff = EnemyDirectionDiff(targetPosition - agent.transform.position);
                    animator.SetFloat("DirectionDiff", directionDiff);
                    transform.root.LookAt(target);
                    if (2 >= Random.Range(1.0f, 100f))
                    {
                        animator.SetTrigger("Attack");
                    }
                }
                )
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("SpacingMove"))
                .Subscribe(_ =>
                {
                    agent.speed = 4f;
                    Vector3 posDiff = transform.position - latestpos;
                    latestpos = transform.position;
                    if (posDiff.magnitude > 0.01f)
                    {
                        animator.SetBool("isMove", true);
                    }
                    else
                    {
                        animator.SetBool("isMove", false);
                    }
                    var directionTarget = (target.position - agent.transform.position).normalized;
                    var targetPosition = target.position - directionTarget * 5;
                    //Debug.Log(targetPosition);
                    agent.SetDestination(targetPosition);
                    float directionDiff = EnemyDirectionDiff(targetPosition - agent.transform.position);
                    animator.SetFloat("DirectionDiff", directionDiff);
                    transform.root.LookAt(target);
                    if (2 >= Random.Range(1.0f, 1000f))
                    {
                        animator.SetTrigger("Attack");
                    }
                }
                )
                .AddTo(this);
            //攻撃0
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("PunchRun"))
                .Subscribe(_ =>
                {
                    agent.speed = 5f;
                    agent.SetDestination(target.position);
                })
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Punch"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(agent.transform.position);
                    transform.root.LookAt(target);
                })
                .AddTo(this);
            //攻撃1
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("JumpAttack"))
                .Subscribe(_ =>
                {
                    agent.speed = 4f;
                })
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("JumpAttack"))
                .Subscribe(_ =>
                {
                    agent.SetDestination(target.position);
                })
                .AddTo(this);
        }
        void Speed()
        {
            agent.speed = 0.01f;
        }
        void EnemyLookAt()
        {
            transform.root.LookAt(target);
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
        float EnemyDirectionDiff(Vector3 moveDirection)
        {
            var targrtDirection = (target.position - agent.transform.position);
            float directionDiff = Vector3.Angle(targrtDirection, moveDirection);
            return directionDiff;
        }
    }

}