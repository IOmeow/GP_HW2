// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    public GameObject hiteffect;
    HealthManager health;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
        health = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(hiteffect,transform.position, Quaternion.identity);
            Destroy(gameObject);
            health.TakeDamage(3f);
        }
    }
}
