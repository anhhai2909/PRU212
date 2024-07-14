using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transitionAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndGame()
    {
        transitionAnim.SetTrigger("End");
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        dataPersistenceManager.SaveToFile(null);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        transitionAnim.SetTrigger("Start");
    }
}
