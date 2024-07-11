using Combat.KnockBack;
using ModifierSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        public Modifiers<Modifier<KnockBackData>, KnockBackData> Modifiers { get; } = new();

        [SerializeField] private float maxKnockBackTime = 0.2f;
        [SerializeField] private float invulnerabilityDuration = 2.0f;
        private bool isInvulnerable = false;

        private bool isKnockBackActive;
        private float knockBackStartTime;

        private Movement movement;
        private CollisionSenses collisionSenses;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }
        private IEnumerator InvulnerabilityCoroutine()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityDuration);
            isInvulnerable = false;
        }

        public void KnockBack(KnockBackData data)
        {
            if (isInvulnerable) return;
            data = Modifiers.ApplyAllModifiers(data);
            
            movement.SetVelocity(data.Strength, data.Angle, data.Direction);
            movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
            StartCoroutine(InvulnerabilityCoroutine());
        }

        private void CheckKnockBack()
        {
            if (isKnockBackActive
                && ((movement.CurrentVelocity.y <= 0.01f && collisionSenses.Ground)
                    || Time.time >= knockBackStartTime + maxKnockBackTime)
               )
            {
                isKnockBackActive = false;
                movement.CanSetVelocity = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            movement = core.GetCoreComponent<Movement>();
            collisionSenses = core.GetCoreComponent<CollisionSenses>();
        }
    }
}