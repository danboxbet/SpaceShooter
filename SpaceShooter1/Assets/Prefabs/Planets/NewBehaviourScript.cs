using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    private void Update()
    {
        Vector3 target = new Vector3(1.0f,1.0f,0.0f);
        transform.position += speed * Time.deltaTime;
    }
}
