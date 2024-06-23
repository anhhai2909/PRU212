using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static DontDestroyScript instance;
    void Start()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
