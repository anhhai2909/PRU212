using System;
using UnityEngine;

namespace ProjectileSystem.Components
{
    /// <summary>
    /// The Movement projectile component is responsible for applying a velocity to the projectile. The velocity can be applied only once upon the projectile
    /// being fired, or can be applied continuously as if self powered. 
    /// </summary>
    public class Movement : ProjectileComponent
    {
        [field: SerializeField] public bool ApplyContinuously { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public float MaxTimeExist { get; private set; }

        private float m_Time;
        // On Init, set projectile velocity once
        protected override void Init()
        {
            base.Init();
            m_Time = MaxTimeExist;
            SetVelocity();
        }

        private void SetVelocity() => rb.velocity = Speed * transform.right;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (!ApplyContinuously)
                return;
            
            SetVelocity();
        }

        protected override void Update()
        {
            base.Update();

            m_Time -= Time.deltaTime;
            if(m_Time <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}