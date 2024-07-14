using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTransitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup canvasGroup;

    [SerializeField] Animator transitionAnim;


    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        transitionAnim.SetTrigger("End");
        transitionAnim.SetTrigger("Start");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }


}
