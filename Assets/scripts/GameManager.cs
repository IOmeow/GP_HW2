// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public Transform player_trans;
    [HideInInspector] public Vector3 player_pos => player_trans? player_trans.position:Vector3.zero;

    [SerializeField] private GameObject shooter_pref;
    [SerializeField] private GameObject bird_pref;
    [SerializeField] private GameObject goblin_pref;

    void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
