using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

namespace Project.AnimatorState
{
    public class MatchEnemyTarget : StateMachineBehaviour
    {
        [SerializeField] private AvatarTarget _targetBodyPart = AvatarTarget.Root;
        [SerializeField, Range(0, 1)] private float _start = 0, _end = 1;

        [HeaderAttribute("match target")]
        private GameObject _targetEnemy;
        [SerializeField] private Vector3 _matchPosition;       // 指定パーツが到達して欲しい座標
        [SerializeField] private Quaternion _matchRotation;    // 到達して欲しい回転

        [HeaderAttribute("Weights")]
        [SerializeField] private Vector3 _positionWeight = Vector3.zero;        // matchPositionに与えるウェイト。(1,1,1)で自由、(0,0,0)で移動できない。e.g. (0,0,1)で前後のみ
        [SerializeField] private float _rotationWeight = 1;            // 回転に与えるウェイト。

        private MatchTargetWeightMask _weightMask;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _weightMask = new MatchTargetWeightMask(_positionWeight, _rotationWeight);
            _targetEnemy = animator.GetComponent<PlayerLockOn>().LockOnEnemy;
            var direction = _targetEnemy.transform.position - animator.transform.position;
            _matchRotation = Quaternion.LookRotation(direction);
            _matchRotation = Quaternion.FromToRotation(animator.transform.rotation.eulerAngles, direction);
            Debug.Log(_matchRotation);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_targetEnemy != null)
                animator.MatchTarget(_matchPosition, _matchRotation, _targetBodyPart, _weightMask, _start, _end);
        }
    }
}