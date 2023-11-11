using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class stonehitcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    private float Timer =2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -=Time.fixedDeltaTime;
        if(Timer<=0)
        {
            Destroy(gameObject);
        }
    }
}
