using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPCList : ScriptableObject
{
    public List<NPCData> NPCDataList = new List<NPCData>();
}
[System.Serializable]
public class NPCData
{
    public string Name = "なまえ";
    public List<ConversationData> ConversationDataList=new List<ConversationData>();
}
[System.Serializable]
public class ConversationData
{
    [Multiline]
    public string conversation;
}
