using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Project.Quest;

public class UIDisplayLastAttack : MonoBehaviour
{
    [SerializeField] private QuestDataCurrent _questDataCurrent;
    void Start()
    {
        var tex = _questDataCurrent.Texture2D;
        // NGUI の UITexture に表示
        RawImage target = GetComponent<RawImage>();
        target.texture = tex;
    }
}
