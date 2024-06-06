using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{

    [SerializeField] private GameObject hitParticles;

    #region Initial Core
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    #endregion

    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Debug.Log(core.transform.parent.name + " taken " + amount + " Damage!");
        Stats?.DecreaseHealth(amount);
        if (!Stats.isAlive())
        {
            Stats?.Deactive();
        }
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive
          && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
                    || Time.time >= knockbackStartTime + maxKnockbackTime)
        )
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
