using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Item.Weapon
{
    [CreateAssetMenu, System.Serializable]
    public class WeaponList : ScriptableObject
    {
        public List<WeaponData> WeaponData = new List<WeaponData>();
    }
}