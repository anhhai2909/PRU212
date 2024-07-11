using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

public class FogScript : MonoBehaviour
{
    public VisualEffect fogEffect;

    private float time;

    private GameObject deathZone;

    private GameObject player;

    private float moveDistance; // Khoảng cách cần di chuyển (1 đơn vị)

    private float moveDuration; // Thời gian để di chuyển (0.5 giây)

    private Vector3 lastPlayerPosition;


    // Start is called before the first frame update
    void Start()
    {
        deathZone = transform.Find("Deathzone").gameObject;
        player = GameObject.Find("Player");
        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
        }
        time = Time.realtimeSinceStartup;
        moveDistance = 1.0f;
        moveDuration = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(deathZone.transform.position.y - player.transform.position.y >= 0.3)
        {
            Debug.Log("Death");
        }
        if(Time.realtimeSinceStartup - time >= 0)
        {
            Vector3 pos = fogEffect.GetVector3("FogPos");
            if(pos != null)
            {
                pos = new Vector3(pos.x, pos.y + 1, pos.z);
                fogEffect.SetVector3("FogPos", pos);
            }
            time += 2f;
            StartCoroutine(MoveOverTime());

        }
    }

    IEnumerator MoveOverTime()
    {
        Vector3 startPosition = deathZone.transform.position; // Vị trí ban đầu của GameObject
        Vector3 endPosition = startPosition + new Vector3(0, moveDistance, 0); // Vị trí kết thúc
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            deathZone.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Chờ đến khung hình tiếp theo
        }

        deathZone.transform.position = endPosition; // Đảm bảo GameObject ở vị trí kết thúc chính xác
    }

 
}
