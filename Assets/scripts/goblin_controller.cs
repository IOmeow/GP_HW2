// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class goblin_controller : monster
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // damage, hp, speed, see_range
        Init(3, 5, 2, 5);
    }

    void FixedUpdate() {
        // no torque
        rb.angularVelocity = Vector3.zero;
        
        // 玩家夠靠近
        if(close_enough()){
            look_at_player();
            rb.velocity = Vector3.Normalize(GameManager.Instance.player_pos - transform.position) * speed;
        }

        // 玩家不夠近
        else{
            rb.velocity = Vector3.zero;
        }
    }
}
