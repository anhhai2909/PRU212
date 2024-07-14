using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpawner : MonoBehaviour
{
    public GameObject clone1; // Reference to the prefab
    public GameObject clone2; // Reference to the prefab
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    protected King king;
    private void Awake()
    {
        king = GetComponent<King>();
        
    }
    public void SpawnClone()
    {

        Vector3 spawnClone1Pos = new Vector3(-9f, 7f, 0f);
        Vector3 spawnClone2Pos = new Vector3(13f, 7f, 0f);

        GameObject spawnedClone1 = Instantiate(clone1, spawnClone1Pos, Quaternion.identity);
        GameObject spawnedClone2 = Instantiate(clone2, spawnClone2Pos, Quaternion.identity);

        Destroy(spawnedClone1, 15f);
        Destroy(spawnedClone2, 15f);
    }

    public void AnimationCloneSpawnEnd()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        king.cloneState.isAliveTimeOver = true;
        rb2d.simulated = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void ShowBossRender()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.simulated = true;
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
    }
}
