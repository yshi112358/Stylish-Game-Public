using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Project.Damage
{
    public interface IDamageable
    {
        float DEF { get; }
        Guid RootGUID { get; }
        DamageObjectType ObjectTypeMyself { get; }
        void AddDamage(int Damage);
    }
}