using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    Vector3 direction;
    public float speed = 2f;
    public float distanceLimit = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Tính toán hướng từ đối tượng đến người chơi
        direction = player.transform.position - transform.position;

        // Tính toán góc quay từ hướng di chuyển
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Cập nhật góc quay của đối tượng
        this.transform.rotation = Quaternion.Euler(0, 0, rot+90);

        // Gọi phương thức Follow để đối tượng di chuyển theo người chơi
        this.Follow();
    }

    void Follow()
    {
        // Tính khoảng cách giữa đối tượng hiện tại và người chơi
        Vector3 distance = this.player.transform.position - transform.position;

        // Xác định điểm đích mà đối tượng sẽ di chuyển đến
        Vector3 targetPoint = this.player.transform.position - distance.normalized * distanceLimit;

        // Di chuyển đối tượng đến điểm đích với tốc độ nhất định
        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position, targetPoint, this.speed * Time.deltaTime);
    }

}
