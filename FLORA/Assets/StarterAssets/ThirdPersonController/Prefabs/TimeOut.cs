using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOut : MonoBehaviour
{
    public float DeathTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,DeathTime);
        
    }

}
