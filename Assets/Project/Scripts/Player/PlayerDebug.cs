using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerDebug : MonoBehaviour
    {
        void OnGUI()
        {
            var stamina = GetComponent<PlayerStamina>();
            var style = new GUIStyle();
            style.fontSize = 30;

            GUILayout.Label($"ST: {stamina.ST.Value}", style);
        }
        private void OnPlayerDebug()
        {
            Debug.Log("Debug!!!");
        }
    }
}
