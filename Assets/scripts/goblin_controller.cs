// using System.Collections;
// using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class goblin_controller : monster
{
    private Rigidbody rb;
    private Animator animator;
    SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // damage, hp, speed, see_range
        Init(3, 5, 2, 5);
        animator.SetBool("attack",false);
        sound = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    void FixedUpdate() {
        // no torque
        rb.angularVelocity = Vector3.zero;
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 玩家夠靠近
        if(close_enough()&&!currentStateInfo.IsName("3attack"))
        {
            look_at_player();
            var target_pos = GameManager.Instance.player_pos - transform.position;
            rb.velocity = Vector3.Normalize(new Vector3(target_pos.x, 0, target_pos.z)) * speed;
            animator.SetBool("walk",true);
        }
        else if(close_enough()&&currentStateInfo.IsName("3attack"))
        {
            look_at_player();
        }

        // 玩家不夠近
        else{
            rb.velocity = Vector3.zero;
            animator.SetBool("walk",false);
            
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            animator.SetBool("attack",true);
            sound.playGoblinSE();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            animator.SetBool("attack",false);
        }
    }
}
