using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptionist : MonoBehaviour
{
    public NPCList NPCList;
    public string name;
    public string Cont;
    void Start()
    {
        name = NPCList.NPCDataList[0].Name;
        Cont = NPCList.NPCDataList[0].ConversationDataList[0].conversation;
    }
    void Update()
    {
        
    }
}
