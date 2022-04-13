using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Merchant : MonoBehaviour
{
    //コマンド用UI
    [SerializeField]
    private GameObject commandUI = null;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OpenCommand()
    {
        commandUI.SetActive(!commandUI.activeSelf);
    }
    //commandscriptから呼び出すコマンド画面の終了
    public void ExitCommand()
    {
        EventSystem.current.SetSelectedGameObject(null);
        commandUI.SetActive(!commandUI.activeSelf);
    }
}
