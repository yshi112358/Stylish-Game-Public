using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Project.Manager;
using TMPro;

public class UIDisplayTimeToResult : MonoBehaviour
{
    [SerializeField] private QuestManager _questManager;
    void Start()
    {
        var time = 20f;
        var text = GetComponent<TextMeshProUGUI>();
        this.UpdateAsObservable()
            .Where(_ => _questManager.QuestClear)
            .Subscribe(_ =>
            {
                if (time >= 0)
                    text.text = "あと" + (int)time + "秒で終了します";
                else
                    gameObject.SetActive(false);
                time -= Time.deltaTime;
            });
        this.UpdateAsObservable()
            .Subscribe(_ => 
            {
                text.canvasRenderer.SetAlpha(_questManager.QuestClear ? 1 : 0);
                text.transform.GetChild(0).gameObject.SetActive(_questManager.QuestClear);
            });
    }
}
