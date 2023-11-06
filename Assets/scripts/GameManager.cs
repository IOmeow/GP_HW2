// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player_trans;
    [HideInInspector] public Vector3 player_pos => player_trans.position;

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
