using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=30)
        {
            Destroy(gameObject);
        }
    }

}
