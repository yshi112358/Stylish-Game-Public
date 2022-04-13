using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Enemy;

namespace Project.Damage
{
    public class DamageTrigger : MonoBehaviour
    {
        void Start()
        {
            var AttackableMyself = GetComponent<IAttackable>();
            AttackableMyself.IsAttack
                .Where(x => !x)//IsAtackがfalseになったら
                .Subscribe(_ => AttackableMyself.HitGUID.Clear())//HitGUIDの初期化
                .AddTo(this);

            this.OnTriggerStayAsObservable()
                .Where(_ => AttackableMyself.IsAttack.Value)//攻撃判定があるか
                .Select(x => x.GetComponent<IDamageable>())
                .Where(x => x != null)//相手はダメージを与えられるオブジェクトであるか
                .Where(x => !AttackableMyself.HitGUID.Contains(x.RootGUID))//既にHitしたオブジェクトではないか
                .Subscribe(x =>
                {
                    var DamageableOpponent = x;
                    if (DamageableOpponent.ObjectTypeMyself == AttackableMyself.ObjectTypeOpponent)
                    {
                        DamageableOpponent.AddDamage(DamageCalculate.Calculate(AttackableMyself.ATK, DamageableOpponent.DEF));
                    }
                    AttackableMyself.HitGUID.Add(x.RootGUID);//HitGUIDに追加
                });
        }
    }
}