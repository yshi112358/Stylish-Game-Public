using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class UIEnemyDamage : MonoBehaviour
    {
        private float _damage;
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = _damage.ToString();
            Destroy(gameObject, 1);
        }
        public void SetDamage(float _damage)
        {
            this._damage = _damage;
        }
    }
}