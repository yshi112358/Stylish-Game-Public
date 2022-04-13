using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Project.Player.InputImpls
{
    public class PlayerInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        [SerializeField] private PlayerCameraSensitivityData data;
        private readonly ReactiveProperty<bool> _jumpButton = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _attackButtonX = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _attackButtonY = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _escapeButton = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<bool> _runButton = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _lockOnButton = new ReactiveProperty<bool>();
        private readonly ReactiveCollection<bool> _weaponButton = new ReactiveCollection<bool> { false, false, false, false };

        public IReadOnlyReactiveProperty<bool> JumpButton => _jumpButton;
        public IReadOnlyReactiveProperty<bool> AttackButtonX => _attackButtonX;
        public IReadOnlyReactiveProperty<bool> AttackButtonY => _attackButtonY;
        public IReadOnlyReactiveProperty<bool> EscapeButton => _escapeButton;
        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _moveDirection;
        public IReadOnlyReactiveProperty<bool> RunButton => _runButton;
        public IReadOnlyReactiveProperty<bool> LockOnButton => _lockOnButton;
        public IReadOnlyReactiveCollection<bool> WeaponButton => _weaponButton;


        private readonly ReactiveProperty<Vector2> _lookDirection = new ReactiveProperty<Vector2>();
        public IReadOnlyReactiveProperty<Vector2> LookDirection => _lookDirection;

        private void Start()
        {
            var inputActions = new PlayerInputActions();
            inputActions.Player.Enable();
            this.UpdateAsObservable()
                .Subscribe(x =>
                {
                    _moveDirection.Value = inputActions.Player.Move.ReadValue<Vector2>();
                    _lookDirection.Value = inputActions.Player.Look.ReadValue<Vector2>() * data.Sensitivity;
                    _runButton.Value = inputActions.Player.Run.IsPressed();
                    _jumpButton.Value = inputActions.Player.Jump.triggered;
                    _escapeButton.Value = inputActions.Player.Escape.triggered;
                    _attackButtonX.Value = inputActions.Player.Attack_X.triggered;
                    _attackButtonY.Value = inputActions.Player.Attack_Y.triggered;
                    _lockOnButton.Value = inputActions.Player.LockOn.triggered;
                    _weaponButton[0] = inputActions.Player.WeaponChanger.ReadValue<Vector2>().y > 0;
                    _weaponButton[1] = inputActions.Player.WeaponChanger.ReadValue<Vector2>().y < 0;
                    _weaponButton[2] = inputActions.Player.WeaponChanger.ReadValue<Vector2>().x < 0;
                    _weaponButton[3] = inputActions.Player.WeaponChanger.ReadValue<Vector2>().x > 0;
                });
        }
    }
}
