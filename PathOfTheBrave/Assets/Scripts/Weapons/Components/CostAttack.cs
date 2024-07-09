using CoreSystem;
using CoreSystem.StatsSystem;
using UnityEngine;

namespace Weapons.Components
{
    public class CostAttack : WeaponComponent<CostAttackData, AttackCostAttack>
    {
        private Stats stats;
        protected override void Awake()
        {
            base.Awake();
            stats = weapon.Core.GetCoreComponent<Stats>();
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();
            if (CheckCanAttack())
            {
                ChangeStats();
                weapon.SetCanAttack(true);
            }
            else
            {
                weapon.SetCanAttack(false);
            }
            Debug.Log(weapon.CanAttack);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected bool CheckCanAttack()
        {
            if (stats != null)
            {
                if (stats.Mana.CurrentValue < currentAttackData.CostMana)
                {
                    Debug.Log("Not enough mana to perform the skill");
                    return false;
                }
                if (stats.Health.CurrentValue < currentAttackData.CostHealth)
                {
                    Debug.Log("Not enough health to perform the skill");
                    return false;
                }
                return true;
            }
            else
            {
                Debug.Log("Can not found Stat in Core System");
                return false;
            }
        }

        protected override void ChangeStats()
        {
            base.ChangeStats();
            Core.GetCoreComponent<Stats>().Health.Decrease(currentAttackData.CostHealth);
            Core.GetCoreComponent<Stats>().Mana.Decrease(currentAttackData.CostMana);
        }
    }
}

