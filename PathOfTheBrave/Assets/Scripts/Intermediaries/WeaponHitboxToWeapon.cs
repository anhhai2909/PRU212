using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weapon != null)
        {
            weapon.AddToDetected(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (weapon != null)
            weapon.RemoveFromDetected(collision);
    }
}
