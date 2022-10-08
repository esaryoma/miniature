using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] public string skillName;
    [SerializeField] public int damage;
    [SerializeField] public int areaOfEffect;
    [SerializeField] public int playerMove;
    [SerializeField] public int targetMove;
    [SerializeField] public int wound;
    [SerializeField] public int range;
    [SerializeField] public Status status;
    [SerializeField] public int pierceArmor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
