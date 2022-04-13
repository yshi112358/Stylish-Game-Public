using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Manager;

namespace Project.Player
{
    public class PlayerBGM : MonoBehaviour
    {
        [SerializeField] private BGMMixer _bgmMixer;
        [SerializeField] private QuestManager _questManager;
        [SerializeField] private AudioClip _fieldBGM;
        [SerializeField] private AudioClip _nearEnemyBGM;
        [SerializeField] private AudioClip _questClearBGM;
        [SerializeField] private AudioClip _questEndBGM;
        [SerializeField] private AudioClip _gameOverBGM;

        void Start()
        {
            var distance = GetComponent<PlayerEnemyDistance>();
            var parameter = GetComponent<PlayerParameters>();
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (_questManager.QuestClear)
                        _bgmMixer.ToNextBGM(_questClearBGM);
                    else if (parameter.HP.Value <= 0)
                        _bgmMixer.ToNextBGM(_gameOverBGM);
                    else if (distance.NearEnemyList.Count > 0 && distance.GetEnemyDistance(distance.NearEnemyList[0]) < 20)
                        _bgmMixer.ToNextBGM(_nearEnemyBGM);
                    else
                        _bgmMixer.ToNextBGM(_fieldBGM);
                }).AddTo(this);


        }
    }
}