using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] public string statusType;
    [SerializeField] public int length;
    
    // a placeholder for now before we actually decide how to encode the resolve timing (start of characters turn, end of turn, when attacked / attacking)
    [SerializeField] public string resolvedWhen;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
