using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerJump : MonoBehaviour
    {
        void Start()
        {
            var input = GetComponent<IInputEventProvider>();
            var rb = GetComponent<Rigidbody>();
            var agent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();

            var judgeGround = GetComponent<PlayerJudgeGround>();

            //ボタンが押された時の処理
            input.JumpButton
                .DistinctUntilChanged()
                .Where(x => x)
                .Subscribe(_ => animator.SetTrigger("Jump"))
                .AddTo(this);

            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("Jump"))
                .Subscribe(_ =>
                {
                    if (agent.enabled)
                    {
                        agent.isStopped = true;
                        agent.enabled = false;
                    }
                    rb.isKinematic = false;
                    rb.velocity += new Vector3(0, 5, 0);
                })
                .AddTo(this);
            var sensitivity = 5;
            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsTag("Air"))
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
                .Where(x => x.StateInfo.IsTag("Air"))
                .Subscribe(_ => rb.MovePosition(transform.forward * animator.GetFloat("Speed") * sensitivity * Time.deltaTime + rb.transform.position))
                .AddTo(this);

            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsName("Land"))
                .Subscribe(_ =>
                {
                    agent.enabled = true;
                    rb.isKinematic = true;
                })
                .AddTo(this);
            //端でジャンプ
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Physics.Raycast(transform.position + transform.forward + transform.up, -transform.up, out RaycastHit hit, Mathf.Infinity, ~(1 << 3)))
                        if (hit.collider.CompareTag("Terrain"))
                            if (transform.InverseTransformPoint(hit.point).y + 0.1f < -0.9f)
                                animator.SetTrigger("JumpEdge");
                });
        }
    }
}