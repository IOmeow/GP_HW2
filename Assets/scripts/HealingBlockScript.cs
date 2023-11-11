using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBlockScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private HealthManager health;
    public float healAmount = 5f;
    void Start(){
        health = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            health.Heal(healAmount);
            Destroy(gameObject);
        }

    }
}
