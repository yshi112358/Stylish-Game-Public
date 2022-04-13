using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;
using Project.Quest;
using Project.Enemy;

namespace Project.Manager
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private SceneTransitionManager _sceneTransitionManager;
        [SerializeField] private QuestList _questList;
        [SerializeField] private QuestDataCurrent _questDataCurrent;
        private System.IDisposable _timeCountDisposable;
        [SerializeField] private List<QuestTarget> _enemyDeadList = new List<QuestTarget>();
        private bool _questClear = false;
        [SerializeField] Animator _audioMixer;

        public SceneTransitionManager SceneTransitionManager => _sceneTransitionManager;
        public QuestList QuestList => _questList;
        public QuestDataCurrent QuestDataCurrent => _questDataCurrent;
        public List<QuestTarget> EnemyDeadList => _enemyDeadList;
        public bool QuestClear => _questClear;

        private void Awake()
        {
            if (_questDataCurrent.QuestData != null && _questDataCurrent.QuestData.MapName == SceneManager.GetActiveScene().name)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                EnemySpawn();
                PlayerSpawn();
                _questDataCurrent.Time = 0;
                _timeCountDisposable = this.UpdateAsObservable()
                    .Subscribe(_ => _questDataCurrent.Time += Time.deltaTime);
                this.UpdateAsObservable()
                    .Select(_ => _questDataCurrent.EnemyList)
                    .Subscribe(x =>
                    {
                        _enemyDeadList.Clear();
                        foreach (var enemyCurrent in x)
                        {
                            var parameter = enemyCurrent.GetComponent<EnemyParameters>();
                            if (parameter.HP.Value <= 0)
                            {
                                var enemyData = parameter.QuestEnemyData.EnemyData;
                                var enemyDead = _enemyDeadList.Find(y => y.TargetEnemy == enemyData);
                                if (enemyDead == null)
                                {
                                    var enemyDeadTarget = new QuestTarget();
                                    enemyDeadTarget.SetEnemy(enemyData);
                                    enemyDeadTarget.SetNumber(1);
                                    _enemyDeadList.Add(enemyDeadTarget);
                                }
                                else
                                {
                                    enemyDead.SetNumber(enemyDead.Number + 1);
                                }
                            }
                        }
                        foreach (var enemyTarget in _questDataCurrent.QuestData.QuestTarget)
                        {
                            var enemyDead = _enemyDeadList.Find(x => x.TargetEnemy == enemyTarget.TargetEnemy);
                            if (enemyDead != null && enemyDead.Number >= enemyTarget.Number)
                            {
                                _questClear = true;
                            }
                            else
                            {
                                _questClear = false;
                                break;
                            }
                        }
                    });
                this.UpdateAsObservable()
                    .Where(_ => _questClear)
                    .Take(1)
                    .Subscribe(_ => QuestEnd());
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        private void EnemySpawn()
        {
            _questDataCurrent.EnemyList.Clear();
            foreach (var questEnemyData in _questDataCurrent.QuestData.QuestEnemyData)
            {
                foreach (var spawnPos in questEnemyData.SpawnPos)
                {
                    var enemyObj = Instantiate(questEnemyData.EnemyData.Prefab, spawnPos, Quaternion.identity);//Enemyの生成
                    enemyObj.name = questEnemyData.EnemyData.Prefab.name;
                    enemyObj.GetComponent<EnemyParameters>().SetEnemyData(questEnemyData);//Enemyの初期化
                    _questDataCurrent.EnemyList.Add(enemyObj);//QuestDataCurrentに追加
                }
            }
        }
        private void PlayerSpawn()
        {
            var Player = GameObject.Find("Player");
            var agent = Player.GetComponent<NavMeshAgent>();
            agent.enabled = false;
            Player.transform.position = _questDataCurrent.QuestData.PlayerPosition;
            agent.enabled = true;
        }

        public void SetQuest(QuestData questData)
        {
            _questDataCurrent.QuestData = questData;
        }
        public void QuestStart(QuestData questData)
        {
            SetQuest(questData);
            SceneTransitionManager.SceneTransitionFade(_questDataCurrent.QuestData.MapName);
        }
        public void QuestEnd()
        {
            _timeCountDisposable.Dispose();
            //スクショ
            var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            var rt = new RenderTexture(screenShot.width, screenShot.height, 24);
            var prev = Camera.main.targetTexture;
            Camera.main.targetTexture = rt;
            Camera.main.Render();
            Camera.main.targetTexture = prev;
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
            screenShot.Apply();
            //データの受け渡し
            _questDataCurrent.Texture2D = screenShot;
            StartCoroutine(ResultCountDown());
        }
        IEnumerator ResultCountDown()
        {
            yield return new WaitForSeconds(20);
            _audioMixer.SetTrigger("FadeOut");
            //QuestClearを出す
            SceneTransitionManager.SceneTransition("Result");
        }
    }
}