using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            sound.playBGM(0);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            sound.playBGM(1);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            sound.playBGM(2);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            sound.playBGM(3);
        }

        if(Input.GetKeyDown(KeyCode.A)){
            sound.playCureSE();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            sound.playVictorySE();
        }

        
    }
}
