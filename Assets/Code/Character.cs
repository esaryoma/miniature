using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public string charName;
    [SerializeField] public int endurance;
    [SerializeField] public int wounds;
    [SerializeField] public int resolve;

    public enum CharacterType 
    {
        Player,
        Enemy
    }

    public CharacterType characterType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
