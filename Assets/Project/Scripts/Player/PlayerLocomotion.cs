using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerLocomotion : MonoBehaviour
    {
        void Start()
        {
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();
            var input = GetComponent<IInputEventProvider>();
            var agent = GetComponent<NavMeshAgent>();
            var rb = GetComponent<Rigidbody>();
            var capsule = GetComponent<CapsuleCollider>();
            var judgeGround = GetComponent<PlayerJudgeGround>();

            //入力
            var direction = Vector3.zero;
            input.MoveDirection
                .Subscribe(x => direction = new Vector3(x.x, 0, x.y))
                .AddTo(this);
            this.UpdateAsObservable()
                .Select(_ => direction.magnitude)
                .Subscribe(x =>
                {
                    if (x > 0)
                        animator.SetFloat("Speed", Mathf.MoveTowards(animator.GetFloat("Speed"), x, 5 * Time.deltaTime));
                    else
                        animator.SetFloat("Speed", Mathf.MoveTowards(animator.GetFloat("Speed"), x, 10 * Time.deltaTime));
                });

            //走る
            input.RunButton
                .Subscribe(x => animator.SetBool("RunButton", x))
                .AddTo(this);

            //移動
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Idle-Walk"))
                .Subscribe(_ => agent.Move(transform.forward * animator.GetFloat("Speed") * 5 * Time.deltaTime))
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Run"))
                .Subscribe(_ => agent.Move(transform.forward * 10 * Time.deltaTime))
                .AddTo(this);

            //回転
            var targetRotation = transform.rotation;
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsTag("Locomotion"))
                .Subscribe(_ =>
                {
                    if (direction.magnitude > 0)
                        targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up) * Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 600 * Time.deltaTime);
                });

            stateMachine
                .OnStateEnterAsObservable()
                .Where(x => x.StateInfo.IsTag("Locomotion"))
                .Subscribe(_ =>
                {
                    agent.enabled = true;
                    rb.isKinematic = true;
                    capsule.enabled = true;
                })
                .AddTo(this);
        }
    }
}