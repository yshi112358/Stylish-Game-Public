using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Project.UI;

namespace Project.Manager
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private GameObject LoadUIPrefab;
        [SerializeField] private UIFadeOut FadeOut;
        public bool IsFadeEnd => FadeOut.IsFadeEnd;
        private Slider slider;
        public void SceneTransition(string Name)
        {
            var LoadUI = Instantiate(LoadUIPrefab);
            slider = LoadUI.GetComponentInChildren<Slider>();
            //　コルーチンを開始
            StartCoroutine(LoadData(Name));
        }

        IEnumerator LoadData(string Name)
        {
            // シーンの読み込みをする
            var async = SceneManager.LoadSceneAsync(Name);

            //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
            while (!async.isDone)
            {
                var progressVal = Mathf.Clamp01(async.progress);
                slider.value = progressVal;
                yield return null;
            }
        }

        Animator animator;
        public void SceneTransitionFade(string Name)
        {
            animator = FadeOut.GetComponent<Animator>();
            animator.SetTrigger("FadeOut");
            //　コルーチンを開始
            StartCoroutine(LoadDataFade(Name));
        }
        IEnumerator LoadDataFade(string Name)
        {
            while (!IsFadeEnd)
            {
                yield return null;
            }
            // シーンの読み込みをする
            var async = SceneManager.LoadSceneAsync(Name);
            var LoadUI = Instantiate(LoadUIPrefab);
            slider = LoadUI.GetComponentInChildren<Slider>();
            //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
            while (!async.isDone)
            {
                var progressVal = Mathf.Clamp01(async.progress);
                slider.value = progressVal;
                yield return null;
            }
        }
    }
}