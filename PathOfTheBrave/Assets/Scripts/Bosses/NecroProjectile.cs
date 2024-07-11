using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroProjectile : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballPos;
    private GameObject currentFireball;
    private Animator animator;
    private float timer = 0f;
    private float delay = 2f; // Adjust this value as needed
    protected Necromancer necromancer;
    private void Awake()
    {
        necromancer = GetComponent<Necromancer>();
    }

    void Start()
    {
        animator = GetComponent<Animator>(); // Initialize animator here
        currentFireball = null;
    }

    void Update()
    {

    }

    public void Shoot()
    {
        if (fireballPrefab != null && fireballPos != null)
        {
            // Instantiate a new fireball
            currentFireball = Instantiate(fireballPrefab, fireballPos.position, Quaternion.identity);
        }
    }

    public void AnimationShootEnd()
    {
        necromancer.rangedAttackState.isCastTimeOver = true;
    }
}
