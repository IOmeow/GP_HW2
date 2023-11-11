// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    public GameObject hiteffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(hiteffect,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
