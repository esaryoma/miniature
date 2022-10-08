using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    [SerializeField] public string reactionName;
    [SerializeField] public int damage;
    [SerializeField] public int areaOfEffect;
    [SerializeField] public int characterMove;
    [SerializeField] public int targetMove;
    [SerializeField] public string moveDirection;
    [SerializeField] public int wound;
    [SerializeField] public int range;
    [SerializeField] public Status status;
    [SerializeField] public int pierceArmor;
    [SerializeField] public int shieldPoints;
    [SerializeField] public string freeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
