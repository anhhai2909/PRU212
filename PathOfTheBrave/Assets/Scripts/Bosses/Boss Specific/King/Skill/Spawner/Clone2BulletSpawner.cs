using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone2BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }


    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            for (int i = 0; i < 8; i++)
            {
                float angle = i * -45f;
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle)) * transform.rotation;
                spawnedBullet.transform.rotation = rotation;
            }
        }
    }
}
