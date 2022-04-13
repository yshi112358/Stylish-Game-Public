using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu,System.Serializable]
public class SaveData : ScriptableObject
{
    public int LV;
    public int HP;
    public int ATK;
    public int WeaponCurrentID;
    public int[] WeaponListCurrent = new int[4];
}