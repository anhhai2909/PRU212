using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    protected King king;
    public GameObject lightBeam;
    public GameObject player;
    public float fixedYPosition = -20f; // Set this to your desired fixed y position

    private void Awake()
    {
        king = GetComponent<King>();
    }

    private void SpawnLightBeam()
    {
        if (lightBeam != null && player != null)
        {
            Vector3 spawnPosition = new Vector3(player.transform.position.x, fixedYPosition, 0);
            GameObject s = Instantiate(lightBeam, spawnPosition, Quaternion.identity);

            Debug.Log(s.transform.position);
        }
        else
        {
            Debug.LogError("lightBeam or player is not assigned.");
        }
    }

    public void AnimationSpawnSpikeEnd()
    {
        king.spikeState.isCastTimeOver = true;
    }

    
}