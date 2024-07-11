using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroProjectile : MonoBehaviour
{
    public GameObject fireball;
    public Transform fireballPos;
    private List<GameObject> fireballList;
    void Start()
    {
        fireballList = new List<GameObject>();
    }

    void Update()
    {
        // Only shoot if there are less than 3 fireballs
        if (fireballList.Count < 1)
        {
            Shoot();
        }

        // Remove null entries (destroyed fireballs) from the list
        fireballList.RemoveAll(item => item == null);
    }

    void Shoot()
    {
        GameObject newFireball = Instantiate(fireball, fireballPos.position, Quaternion.identity);
        fireballList.Add(newFireball);
    }
}
