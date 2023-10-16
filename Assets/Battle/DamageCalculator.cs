// 日本語対応
using System;
using Sky.Actor;

namespace Sky
{
    namespace Battle
    {
        public static class DamageCalculator
        {
            public static int GetAttackCalculationResult(ActorModel actionActor, ActorModel targetActor)
            {
                // 一旦 行動アクターの攻撃力分返す。
                var damageValue = actionActor.TotalAttributeStatus.Attack;
                return damageValue;
            }
        }
    }
}