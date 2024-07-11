namespace Weapons.Components
{
    public class CostAttackData : ComponentData<AttackCostAttack>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(CostAttack);
        }
    }
}

