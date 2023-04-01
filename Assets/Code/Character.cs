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
    [SerializeField] public List<Status> immunities; 

    public enum CharacterType 
    {
        Player,
        Enemy
    }

    public CharacterType characterType;

    public Sprite characterSprite;
    public Sprite figureSprite;
    public enum CharacterRace
    {
        Human,
        Orc
    }

    public CharacterRace characterRace;


    public List<Status> addToStatus(Status status) {
        statuses.Add(status);
        return statuses;
    }

    public List<Status> getImmunities() {
        return immunities;
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
