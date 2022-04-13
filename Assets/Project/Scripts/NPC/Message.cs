using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
 
public class Message : MonoBehaviour
{ 
    //メッセージUI
    private Text messageText;
    [SerializeField]
    [TextArea(1, 20)]
    public static string allMessage;
    //使用する分割文字列
    [SerializeField]
    private string splitString = "<>";
    //分割したメッセージ
    private string[] splitMessage;
    //分割したメッセージの何番目か
    private int messageNum;
    //テキストスピード
    [SerializeField]
    private float textSpeed = 0.05f;
    //　経過時間
    private float elapsedTime = 0f;
    //　今見ている文字番号
    private int nowTextNum = 0;
    //　マウスクリックを促すアイコン
    private Image clickIcon;
    //　クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //　1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    private bool isEndMessage = false;
    void Start()
    {
        clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
        clickIcon.enabled = false;
        messageText = GetComponentInChildren<Text>();
        messageText.text = "";
        SetMessage(allMessage);
        InputManager.inputState = InputManager.InputState.UI;
    }
    void Update()
    {
        //　メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //　1回に表示するメッセージを表示していない	
        if (!isOneMessage)
        {
            //　テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                messageText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                //　メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //　メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (InputManager.inputActions.UI.Decide.triggered)
            {
                //　ここまでに表示しているテキストに残りのメッセージを足す
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //　1回に表示するメッセージを表示した
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //　クリックアイコンを点滅する時間を超えた時、反転させる
            if (elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //　マウスクリックされたら次の文字表示処理
            if (InputManager.inputActions.UI.Decide.triggered)
            {
                nowTextNum = 0;
                messageNum++;
                messageText.text = "";
                clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;

                //　メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (messageNum >= splitMessage.Length)
                {
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    InputManager.inputState = InputManager.InputState.Player;
                }
            }
        }
    }
    //　新しいメッセージを設定
    void SetMessage(string message)
    {
        allMessage = message;
        //　分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }
    //　他のスクリプトから新しいメッセージを設定しUIをアクティブにする
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
