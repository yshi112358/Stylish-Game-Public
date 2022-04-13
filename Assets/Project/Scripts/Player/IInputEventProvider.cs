using UniRx;
using UnityEngine;

namespace Project.Player
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<bool> JumpButton { get; }
        IReadOnlyReactiveProperty<bool> AttackButtonX { get; }
        IReadOnlyReactiveProperty<bool> AttackButtonY { get; }
        IReadOnlyReactiveProperty<bool> EscapeButton { get; }
        IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
        IReadOnlyReactiveProperty<bool> RunButton { get; }
        IReadOnlyReactiveCollection<bool> WeaponButton { get; }
    }
}