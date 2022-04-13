using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Project.Damage
{
    public interface IAttackable
    {
        float ATK { get; }
        IReadOnlyReactiveProperty<bool> IsAttack { get; }
        List<Guid> HitGUID { set; get; }
        DamageObjectType ObjectTypeOpponent { get; }
    }
}