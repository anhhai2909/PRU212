using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static LoadDataScript instance;
    
    public static float GetHealth()
    {
        return 100;
    }

    public static float GetCoin()
    {
        return 1000;
    }


}
