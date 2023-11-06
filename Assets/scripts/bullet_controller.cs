// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") Destroy(gameObject);
    }
}
