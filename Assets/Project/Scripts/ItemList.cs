using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemList : ScriptableObject
{
    public List<ItemData> ItemDataList = new List<ItemData>();
}
[System.Serializable]
public class ItemData
{
    public string Name;
    public int ID;
}
