using Combat.Damage;
using UnityEngine;
using Utilities;

public class WeaponScript : MonoBehaviour
{
    public float damage;
    public Vector2 knockbackAngle;
    public float knockbackStrength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit melee");
            if (collision.gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
            }

            if (collision.gameObject.TryGetComponentInChildren(out IKnockBackable knockBackable))
            {
                if (gameObject.GetComponentInParent<BlackSmithMovement>())
                {
                    knockBackable.KnockBack(new Combat.KnockBack.KnockBackData(knockbackAngle,
                                        knockbackStrength, gameObject.GetComponentInParent<BlackSmithMovement>().isFacingRight ? 1 : -1, gameObject));
                }
                if (gameObject.GetComponentInParent<ArcherMovement>())
                {
                    knockBackable.KnockBack(new Combat.KnockBack.KnockBackData(knockbackAngle,
                                        knockbackStrength, gameObject.GetComponentInParent<ArcherMovement>().isFacingRight ? 1 : -1, gameObject));
                }
            }
        }
    }
}
