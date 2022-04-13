using UnityEngine;
using UniRx;
using Project.Item;

namespace Project.Item.Weapon
{
    [System.Serializable]
    public class WeaponData
    {
        public string Name;
        public int ATK;
        public int ID;
        public GameObject Prefab;
    }
}