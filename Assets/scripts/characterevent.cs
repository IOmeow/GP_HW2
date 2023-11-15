using System.Collections;
using System.Collections.Generic;
using StarterAssets;
//using UnityEditor.VersionControl;
using UnityEngine;

public class characterevent : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    GameObject player;
    public GameObject WEFprefab;
    public GameObject Weapon;
    Vector3 playerposition;
    float Timer=0;
    public float additionalAngle = 30.0f;
    bool attack;
    SoundManager sound;
    void Start()
    {
        player = gameObject;
        sound = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
         // 获取当前的动画状态
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        if(Input.GetMouseButtonDown(0)&&Timer<=0&&!currentState.IsName("normal attack"))
        {
            animator.SetBool("attack",true);
            Timer = 0.75f;
            ThirdPersonController.MoveSpeed=0;
            sound.playAttackSE();
        }
        else if(Timer>0&&currentState.IsName("normal attack"))
        {
            Timer-= Time.fixedDeltaTime;
            if(Timer<=0.3f&&attack ==false)
            {
                playerposition = this.transform.position;
                GameObject newObject=Instantiate(WEFprefab,new Vector3(playerposition.x,playerposition.y+0.4f,playerposition.z),this.transform.rotation*Quaternion.Euler(0,-40,additionalAngle));
                newObject.transform.localScale=new Vector3(-1 * newObject.transform.localScale.x, newObject.transform.localScale.y, newObject.transform.localScale.z);
                attack=true;
            }       
        }
        else if(Timer<=0)
        {
            animator.SetBool("attack",false);
            ThirdPersonController.MoveSpeed=4;
            attack=false;
        }
        }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var otherObject = other.gameObject;
    //    var script = otherObject.GetComponent<goblin_controller>();
    //    print(script);
    //}
}

