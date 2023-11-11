using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    float time=1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,time);

    }
}

