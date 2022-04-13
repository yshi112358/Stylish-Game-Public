using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerEscape : MonoBehaviour
    {
        private bool _invincible = false;
        public bool Invincible => _invincible;
        void Start()
        {
            var input = GetComponent<IInputEventProvider>();
            var agent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();

            var judgeGround = GetComponent<PlayerJudgeGround>();

            //ボタンが押された時の処理
            input.EscapeButton
                .DistinctUntilChanged()
                .Where(x => x)
                .Subscribe(_ => animator.SetTrigger("Escape"))
                .AddTo(this);
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Escape"))
                .Subscribe(_ => agent.Move(transform.forward * animator.GetFloat("PositionForward") * 10 * Time.deltaTime))
                .AddTo(this);
        }
        private void OnEscapeEnter()
        {
            _invincible = true;
        }
        private void OnEscapeExit()
        {
            _invincible = false;
        }
    }
}