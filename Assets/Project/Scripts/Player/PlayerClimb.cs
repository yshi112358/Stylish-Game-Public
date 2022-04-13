using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerClimb : MonoBehaviour
    {
        void Start()
        {
            var input = GetComponent<IInputEventProvider>();
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            var agent = GetComponent<NavMeshAgent>();
            var rb = GetComponent<Rigidbody>();
            var capsule = GetComponent<CapsuleCollider>();
            var judgeGround = GetComponent<PlayerJudgeGround>();

            input.MoveDirection
                .Subscribe(x =>
                {
                    animator.SetFloat("AxisLx", x.magnitude * x.x / (x.y != 0 ? Mathf.Abs(x.y) : 1));
                    animator.SetFloat("AxisLy", x.magnitude * x.y / (x.x != 0 ? Mathf.Abs(x.x) : 1));
                })
                .AddTo(this);
            this.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitFoot, 0.5f, ~(1 << 3))
                            && hitFoot.collider.CompareTag("Terrain")
                            && Physics.Raycast(transform.position + new Vector3(0, 0.4f, 0), transform.forward, out RaycastHit hitKnee, 0.5f, ~(1 << 3))
                            && hitKnee.collider.CompareTag("Terrain"))
                        {
                            animator.SetTrigger("Climb");
                        }

                        if (Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out RaycastHit hitChest, 1f, ~(1 << 3 | 1 << 6))
                            && hitChest.collider.CompareTag("Terrain"))
                        {
                            animator.ResetTrigger("ClimbLand");
                        }
                        else
                        {
                            animator.SetTrigger("ClimbLand");
                        }
                    });
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsTag("Climb"))
                .Subscribe(_ =>
                {
                    if (agent.enabled)
                    {
                        agent.isStopped = true;
                        agent.enabled = false;
                    }
                    rb.useGravity = false;
                    rb.isKinematic = false;
                    capsule.enabled = false;
                })
                .AddTo(this);
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("Crouch"))
                .Subscribe(_ =>
                {
                    if (agent.enabled)
                    {
                        agent.isStopped = true;
                        agent.enabled = false;
                    }
                    rb.useGravity = true;
                    rb.isKinematic = false;
                })
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Climb-Idle"))
                .Where(x => x.Animator.GetFloat("Speed") == 0)
                .Subscribe(_ => rb.velocity = Vector3.zero)
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("ClimbFast"))
                .Subscribe(_ => rb.MovePosition(transform.up * Time.deltaTime + rb.transform.position))
                .AddTo(this);
        }
    }
}