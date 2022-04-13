using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerStamina : MonoBehaviour
    {
        private readonly ReactiveProperty<float> _st = new ReactiveProperty<float>(100);
        private readonly ReactiveProperty<int> _maxST = new ReactiveProperty<int>(100);
        public IReadOnlyReactiveProperty<float> ST => _st;
        public IReadOnlyReactiveProperty<int> MaxST => _maxST;

        void Start()
        {
            var animator = GetComponent<Animator>();
            var stateMachine = animator.GetBehaviour<ObservableStateMachineTrigger>();

            //スタミナ回復
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x =>
                x.StateInfo.IsName("Idle-Walk")
                || x.StateInfo.IsName("Climb-Idle")
                || x.StateInfo.IsName("Tired")
                || x.StateInfo.IsName("TiredLoop"))
                .Where(_ => ST.Value < 100)
                .Subscribe(_ => _st.Value += 10 * Time.deltaTime)
                .AddTo(this);
            //スタミナ消費
            stateMachine
                .OnStateUpdateAsObservable()
                .Where(x => x.StateInfo.IsName("Run"))
                .Where(_ => ST.Value > 0)
                .Subscribe(_ => _st.Value -= 20 * Time.deltaTime)
                .AddTo(this);
            //スタミナ反映
            ST.DistinctUntilChanged()
                .Subscribe(x => animator.SetFloat("ST", x))
                .AddTo(this);
            //スタミナ切れ
            ST.DistinctUntilChanged()
                .Select(x => x <= 0)
                .Subscribe(x => animator.SetBool("IsMinST", x))
                .AddTo(this);
            //スタミナ最大時
            ST.DistinctUntilChanged()
                .Select(x => x >= MaxST.Value)
                .Subscribe(x => animator.SetBool("IsMaxST", x))
                .AddTo(this);
            //スタミナ範囲設定
            ST.Subscribe(x => _st.Value = Mathf.Clamp(x, 0, MaxST.Value));
        }
    }
}