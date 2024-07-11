using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandle : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
    }

    void Start()
    {
     //   transitionAnim.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrapDeath") || collision.gameObject.CompareTag("Thorn"))
        {
            Death();
        }
    }

    public void Death()
    {
        transitionAnim.Play("DeathTransitionEnd");

        // transitionAnim.gameObject.SetActive(true);
        transitionAnim.SetTrigger("Start");
        transitionAnim.SetTrigger("End");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        // transitionAnim.gameObject.SetActive(false);

    }

   
}
