using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private EnemyParameters EnemyParameters => GetComponent<EnemyParameters>();
        private EnemyData EnemyData => EnemyParameters.QuestEnemyData.EnemyData;

        private void OnAttackEnter(EnemyAttackPart enemyAttackPart)
        {
            var EnemyPartParameters = EnemyData.FindEnemyPartParameters(partName: enemyAttackPart.PartName);
            EnemyPartParameters.SetATK(enemyAttackPart.ATK);
            EnemyPartParameters.SetIsAttack(true);
        }
        private void OnAttackExit(string _partName)
        {
            var EnemyPartParameters = EnemyData.FindEnemyPartParameters(partName: _partName);
            EnemyPartParameters.SetIsAttack(false);
        }
    }
}