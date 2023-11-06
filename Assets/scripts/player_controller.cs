//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    // for player walking and rotate
    private Rigidbody rb;
    private float speed = 5;
    private Vector3 target_dir;
    private float rotate_ratio = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        target_dir = get_direction();
    }

    private void FixedUpdate() {
        walk(target_dir);
        rotate(target_dir);
    }

    // 取得user input, 並計算玩家方向
    private Vector3 get_direction(){
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        return Vector3.Normalize(new Vector3(h, 0, v));
    }

    // 玩家移動
    private void walk(Vector3 dir){
        rb.velocity = dir * speed + new Vector3(0, rb.velocity.y, 0);
    }

    // 玩家旋轉
    private void rotate(Vector3 dir){
        // no torque
        rb.angularVelocity = Vector3.zero;

        // no user input, don't rotate
        if(dir == Vector3.zero) return;

        var target_rot_y = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
        var rot = target_rot_y - transform.eulerAngles.y;

        // -180 <= rot <= 180
        rot = rot > 180 ? (rot - 360) : rot < -180 ? (rot + 360) : rot;

        // rotate
        transform.Rotate(0, rot * rotate_ratio, 0);
    }
}
