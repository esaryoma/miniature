using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // name visible on the card
    [SerializeField] public string charName;
    // hp
    [SerializeField] public int endurance;
    // most regular characters die after 1 wound
    [SerializeField] public int wounds;
    // resource for skills and abilities
    [SerializeField] public int resolve;
    [SerializeField] public List<Status> statuses;


    public enum CharacterType 
    {
        Player,
        Enemy
    }

    public CharacterType characterType;

    public void addToStatus(Status status) {
        statuses.Add(status);
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
