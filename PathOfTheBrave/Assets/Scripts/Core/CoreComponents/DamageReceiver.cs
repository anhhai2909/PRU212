using Combat.Damage;
using ModifierSystem;
using System.Collections;
using UnityEngine;

namespace CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;
        [SerializeField] private GameObject posParticles;
        [SerializeField] private float invulnerabilityDuration = 2.0f;
        private bool isInvulnerable = false;
        /*
         * Modifiers allows us to perform some custom logic on our DamageData before we apply it here. An example where this is being used is by the Block weapon component.
         * Blocking works by assigning a modifier during the active block window of the shield that reduces the amount of damage the player will take. For example: If a shield
         * has a damage absorption property of 0.75 and we deal 10 damage, only 2.5 will actually end up getting removed from player stats after applying the modifier.
         */
        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();

        private Stats stats;
        private ParticleManager particleManager;


        public void Damage(DamageData data)
        {
            if (isInvulnerable) return;
            if (!core.isDashing)
            {
                //print($"Damage Amount Before Modifiers: {data.Amount}");

                // We must apply the modifiers before we do anything else with data. If there are no modifiers currently active, data will remain the same
                data = Modifiers.ApplyAllModifiers(data);

                //print($"Damage Amount After Modifiers: {data.Amount}");

                if (data.Amount <= 0f)
                {
                    return;
                }

                stats.Health.Decrease(data.Amount);
                StartCoroutine(InvulnerabilityCoroutine());
                particleManager.StartWithRandomRotation(damageParticles, posParticles);
                //Instantiate(damageParticles, posParticles.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            }
        }

        private IEnumerator InvulnerabilityCoroutine()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityDuration);
            isInvulnerable = false;
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}