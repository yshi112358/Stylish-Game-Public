using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EquipCommand : MonoBehaviour
{
    public enum CommandMode
    {
        CommandPanel,
        EquipPanelSelect,
        EquipListPanel,
        BuyCheckPanel

    }
    private CommandMode currentCommand;
    //　鍛冶屋コマンドスクリプト
    private Merchant Merchant;
    //　最初に選択するButtonのTransform
    private GameObject firstSelectButton;
    //　2番目に選択するButtonのTransform
    private GameObject seccondSelectButton;
    //　コマンドパネル
    private GameObject commandPanel;
    //　何の装備を選択するか
    private GameObject EquipSelectPanel;
    //　選択した装備の一覧を表示するパネル
    private GameObject EquipListPanel;
    //  購入するかどうかの確認パネル
    private GameObject BuyCheckPanel;
    //　武器ボタンを表示する場所
    private GameObject content;
    //　情報表示パネル
    private GameObject informationPanel;
    //　装備品のボタンのプレハブ
    [SerializeField]
    private GameObject EquipPanelButtonPrefab = null;


    //　コマンドパネルのCanvasGroup
    private CanvasGroup commandPanelCanvasGroup;
    //　装備の種類を選択するパネルのCanvasGroup
    private CanvasGroup EquipSelectPanelCanvasGroup;
    // 　選択された装備品の一覧を表示するパネルのCanvasGroup（武器・防具・アクセ）
    private CanvasGroup EquipListPanelCanvasGroup;
    //  購入するかどうかの確認パネルのCanvasGroup
    private CanvasGroup BuyCheckPanelCanvasGroup;

    //　装備する種類名（武器とか防具とか）
    private Text BlackSmithNameText;
    //　情報表示タイトルテキスト
    private Text informationTitleText;
    //　情報表示テキスト
    private Text informationText;

    //[SerializeField]
    //private PartyStatus partyStatus = null;
    [SerializeField]
    private ItemList ItemList = null;

    //　装備選択のボタンのプレハブ
    //[SerializeField]
    //  private GameObject BlackSmithPanelButtonPrefab = null;

    //　装備品購入時のボタンのプレハブ
    [SerializeField]
    private GameObject BuyCheckButtonPrefab = null;


    //　武器一覧
    private List<GameObject> EquipPanelButtonList = new List<GameObject>();

    void Awake()
    {

        //　コマンド画面を開く処理をしているBlackSmithCommandScriptを取得
        Merchant = GameObject.FindWithTag("Villager").GetComponent<Merchant>();

        //　現在のコマンドを初期化
        currentCommand = CommandMode.CommandPanel;
        //　階層を辿ってを取得
        firstSelectButton = transform.Find("CommandPanel/BuyButton").gameObject;
        seccondSelectButton = transform.Find("EquipSelectPanel/WeaponButton").gameObject;

        //　パネル系
        commandPanel = transform.Find("CommandPanel").gameObject;
        EquipSelectPanel = transform.Find("EquipSelectPanel").gameObject;
        EquipListPanel = transform.Find("EquipListPanel").gameObject;
        content = EquipListPanel.transform.Find("Mask/Content").gameObject;
        BuyCheckPanel = transform.Find("BuyCheckPanel").gameObject;
        informationPanel = transform.Find("InformationPanel").gameObject;


        //　CanvasGroup
        commandPanelCanvasGroup = commandPanel.GetComponent<CanvasGroup>();
        EquipSelectPanelCanvasGroup = EquipSelectPanel.GetComponent<CanvasGroup>();
        EquipListPanelCanvasGroup = EquipListPanel.GetComponent<CanvasGroup>();
        BuyCheckPanelCanvasGroup = BuyCheckPanel.GetComponent<CanvasGroup>();


        //　何の武器か
        BlackSmithNameText = EquipListPanel.transform.Find("EquipNamePanel/Text").GetComponent<Text>();
        //　情報表示用テキスト
        informationTitleText = informationPanel.transform.Find("Title").GetComponent<Text>();
        informationText = informationPanel.transform.Find("Information").GetComponent<Text>();


    }

    private void OnEnable()
    {
        //　現在のコマンドの初期化
        currentCommand = CommandMode.CommandPanel;
        //　コマンドメニュー表示時に他のパネルは非表示にする
        EquipSelectPanel.SetActive(false);
        EquipListPanel.SetActive(false);
        BuyCheckPanel.SetActive(false);
        informationPanel.SetActive(false);




        commandPanelCanvasGroup.interactable = true;
        EquipSelectPanelCanvasGroup.interactable = false;
        BuyCheckPanelCanvasGroup.interactable = false;

        EventSystem.current.SetSelectedGameObject(firstSelectButton);

    }
    void Start()
    {
        
    }

    void Update()
    {
        //　キャンセルボタンを押した時の処理
        if (Input.GetButtonDown("Cancel"))
        {
            //　コマンド選択画面時
            if (currentCommand == CommandMode.CommandPanel)
            {
                Merchant.ExitCommand();//コマンドを閉じる
                gameObject.SetActive(false);

            }
            else if (currentCommand == CommandMode.EquipPanelSelect)
            {
                EquipSelectPanelCanvasGroup.interactable = false;
                EquipSelectPanel.SetActive(false);

                commandPanelCanvasGroup.interactable = true;
                currentCommand = CommandMode.CommandPanel;
                EventSystem.current.SetSelectedGameObject(firstSelectButton);


            }
            else if (currentCommand == CommandMode.EquipListPanel)
            {
                EquipListPanelCanvasGroup.interactable = false;
                EquipListPanel.SetActive(false);

                informationPanel.SetActive(false);

                //　装備品パネルを削除
                for (int i = content.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(content.transform.GetChild(i).gameObject);
                }


                EventSystem.current.SetSelectedGameObject(seccondSelectButton);
                EquipSelectPanelCanvasGroup.interactable = true;
                currentCommand = CommandMode.EquipPanelSelect;

            }
        }
    }
    //　選択したコマンドで処理を分ける
    public void SelectCommand(string command)
    {
        if (command == "Buy")//購入ボタンが選択
        {
            currentCommand = CommandMode.EquipPanelSelect;

            commandPanelCanvasGroup.interactable = false;

        }
        if (command == "Sell")//売却ボタンが選択
        {
            currentCommand = CommandMode.EquipPanelSelect;

            commandPanelCanvasGroup.interactable = false;

        }


        //　階層を一番最後に並べ替え
        EquipSelectPanel.transform.SetAsLastSibling();
        EquipSelectPanel.SetActive(true);
        EquipSelectPanelCanvasGroup.interactable = true;
        EventSystem.current.SetSelectedGameObject(seccondSelectButton);

    }

    public void WeaponSelectCommand(string command)
    {
        if (command == "buki")//武器ボタンが押された
        {
            currentCommand = CommandMode.EquipListPanel;
            EquipSelectPanelCanvasGroup.interactable = false;

            BlackSmithNameText.text = ("武器一覧");

            GameObject EquipButtonIns;

            //　武器リストのボタンを作成
            /*foreach (var weapon in weaponList.WeaponList)
            {
                EquipButtonIns = Instantiate<GameObject>(EquipPanelButtonPrefab, content.transform);
                EquipButtonIns.transform.Find("EquipText").GetComponent<Text>().text = weapon.WeaponName;
                EquipButtonIns.transform.Find("GoldText").GetComponent<Text>().text = weapon.Gold.ToString();
                EquipButtonIns.GetComponent<EquipButtonScript>().SetParam(weapon);
                // EquipButtonIns.GetComponent<Button>().onClick.AddListener(() => CreateItemPanelButton(member));
            }*/

        }
        if (command == "bougu")//防具ボタンが押された
        {
            currentCommand = CommandMode.EquipListPanel;
            EquipSelectPanelCanvasGroup.interactable = false;
        }

        if (command == "akuse")
        {
            currentCommand = CommandMode.EquipListPanel;
            EquipSelectPanelCanvasGroup.interactable = false;
        }


        EquipListPanel.transform.SetAsLastSibling();
        EquipListPanel.SetActive(true);
        EquipListPanelCanvasGroup.interactable = true;
        informationPanel.SetActive(true);

    }
}
