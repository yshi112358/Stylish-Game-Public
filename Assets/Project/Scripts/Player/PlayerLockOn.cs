using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UniRx;
using UniRx.Triggers;
using Project.Quest;
using Project.Enemy;

namespace Project.Player
{
    public class PlayerLockOn : MonoBehaviour
    {
        [SerializeField] private QuestDataCurrent _questDataCurrent;
        [SerializeField] private GameObject _lockOnCamera;
        [SerializeField] private GameObject _lockOnEnemy;
        private readonly ReactiveProperty<bool> _isLockOn = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<bool> IsLockOn => _isLockOn;
        public GameObject LockOnEnemy => _lockOnEnemy;

        void Start()
        {
            var input = GetComponent<InputImpls.PlayerInputEventProvider>();
            var distance = GetComponent<PlayerEnemyDistance>();

            input.LockOnButton
                .Where(x => x)
                .Subscribe(_ =>
                {
                    if (!IsLockOn.Value)
                    {
                        var enemyList = distance.NearEnemyList;
                        if (enemyList != null)
                        {
                            var rect = new Rect(0, 0, Screen.width, Screen.height);
                            foreach (var enemyObj in enemyList)
                                if (distance.GetEnemyDistance(enemyObj) < 20 && rect.Contains(Camera.main.WorldToScreenPoint(enemyObj.transform.position)))
                                {
                                    _lockOnEnemy = enemyObj;
                                    break;
                                }
                            if (_lockOnEnemy != null)
                            {
                                _lockOnCamera.GetComponent<CinemachineVirtualCamera>().LookAt = _lockOnEnemy.transform;
                                _lockOnCamera.SetActive(true);
                                _isLockOn.Value = true;
                            }
                        }
                    }
                    else
                    {
                        LockOff();
                    }
                })
                .AddTo(this);
            this.UpdateAsObservable()
                .Where(_ => _lockOnEnemy != null)
                .Where(_ => _lockOnEnemy.GetComponent<EnemyParameters>().HP.Value < 0)
                .Subscribe(_ => LockOff());
        }
        private void LockOff()
        {
            _lockOnCamera.SetActive(false);
            _lockOnEnemy = null;
            _isLockOn.Value = false;
        }
    }
}