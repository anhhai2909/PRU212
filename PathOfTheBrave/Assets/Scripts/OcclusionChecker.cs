using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionChecker : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; 
    public GameObject fog; 
    public Camera mainCamera;

    void Start()
    {
        player = GameObject.Find("Player");

    }

    void Update()
    {
        if (IsPlayerOccluded())
        {
            Debug.Log("Player is fully occluded by the blocking object!");
        }
    }

    bool IsPlayerOccluded()
    {
        Vector3[] directions = GenerateDirections();
        foreach (var dir in directions)
        {
            Ray ray = new Ray(mainCamera.transform.position, dir);
            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                return false; // Nếu có ray nào không bị chặn, player không bị che hoàn toàn
            }

            if (hit.collider.gameObject != fog)
            {
                return false; // Nếu vật chặn không phải là blockingObject, player không bị che hoàn toàn
            }
        }
        return true; // Tất cả các ray đều bị chặn bởi blockingObject, player bị che hoàn toàn
    }

    Vector3[] GenerateDirections()
    {
        Vector3 playerPos = player.transform.position;
        Vector3[] directions = new Vector3[5];

        directions[0] = playerPos - mainCamera.transform.position; // Trung tâm
        directions[1] = (playerPos + new Vector3(0.5f, 0.5f, 0)) - mainCamera.transform.position; // Trên phải
        directions[2] = (playerPos + new Vector3(-0.5f, 0.5f, 0)) - mainCamera.transform.position; // Trên trái
        directions[3] = (playerPos + new Vector3(0.5f, -0.5f, 0)) - mainCamera.transform.position; // Dưới phải
        directions[4] = (playerPos + new Vector3(-0.5f, -0.5f, 0)) - mainCamera.transform.position; // Dưới trái

        return directions;
    }
}
