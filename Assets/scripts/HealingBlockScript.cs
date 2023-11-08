using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBlockScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(Player.tag))
        {
            Destroy(gameObject);
        }

    }
}
