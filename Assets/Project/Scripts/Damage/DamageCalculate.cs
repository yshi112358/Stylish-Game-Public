using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Damage
{
    public static class DamageCalculate
    {
        public static int Calculate(float ATK, float DEF)
        {
            return (int)(ATK * DEF);
        }
    }
}