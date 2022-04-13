using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerLook : MonoBehaviour, AxisState.IInputAxisProvider
    {
        [SerializeField] private InputImpls.PlayerInputEventProvider input;
        public float GetAxisValue(int axis)
        {
            switch (axis)
            {
                case 0: return input.LookDirection.Value.x;
                case 1: return input.LookDirection.Value.y;
            }
            return 0;
        }
    }
}