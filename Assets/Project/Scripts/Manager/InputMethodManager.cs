using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Project.Manager
{
    public class InputMethodManager : MonoBehaviour
    {
        UnityEngine.InputSystem.PlayerInput _controls;
        private static ReactiveProperty<ControlDeviceType> _currentControlDevice = new ReactiveProperty<ControlDeviceType>();
        public static IReadOnlyReactiveProperty<ControlDeviceType> CurrentControlDevice => _currentControlDevice;
        public enum ControlDeviceType
        {
            KeyboardAndMouse,
            Gamepad,
        }
        void Start()
        {
            _controls = GetComponent<UnityEngine.InputSystem.PlayerInput>();
            _controls.onControlsChanged += OnControlsChanged;
        }
        private void OnControlsChanged(UnityEngine.InputSystem.PlayerInput obj)
        {
            if (obj.currentControlScheme == "Gamepad")
            {
                if (_currentControlDevice.Value != ControlDeviceType.Gamepad)
                {
                    _currentControlDevice.Value = ControlDeviceType.Gamepad;
                }
            }
            else
            {
                if (_currentControlDevice.Value != ControlDeviceType.KeyboardAndMouse)
                {
                    _currentControlDevice.Value = ControlDeviceType.KeyboardAndMouse;
                }
            }
        }
    }
}