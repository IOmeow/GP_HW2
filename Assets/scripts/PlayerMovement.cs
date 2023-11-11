using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float movespeed = 7f;
    // [SerializeField]
    // private float jumpForce = 8f;
    [SerializeField]
    private bool isControllable = true;
    Vector3 playerMovement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isControllable)
            return;

        float h = Input.GetAxisRaw("Horizontal")* movespeed;
        float moveVertical = Input.GetAxisRaw("Vertical") * movespeed;
        rb.velocity = (transform.forward * moveVertical) + (transform.right * h) + (transform.up * rb.velocity.y);
      

    }
}
