// 日本語対応
using Actor;

namespace Battle
{
    public static class DamageCalculator
    {
        public static void Attack(this ActorModel from, ActorModel to)
        {
            var damageValue = from.TotalAttributeStatus.Attack; // 攻撃者の攻撃力を取得。

            to.Damage(damageValue);

            UnityEngine.Debug.Log(from.Name + "の攻撃によって" + to.Name + "へ" + damageValue + " ダメージ！");
            UnityEngine.Debug.Log(to.Name + "の残りHP: " + to.ResourceStatus.Hp);
        }
    }
}