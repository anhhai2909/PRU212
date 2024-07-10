using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour
{
    public RuntimeAnimatorController falling;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            AnimatorStateInfo stateInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("FallingRock") && stateInfo.normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.AddComponent<Animator>();
            gameObject.GetComponent<Animator>().runtimeAnimatorController = falling;
        }
    }
}
