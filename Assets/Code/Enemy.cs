using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public enum State
    {
        Hunter,
        Patrol,
        Guard
    }

    public enum CharacterClass
    {
        Melee,
        Ranged,
        Magic
    }

    public CharacterClass characterClass;

    // how to switch between reaction/behaviorstrategies when enemy switces States (hunter, patrol, guard)?
    // reactions/behaviors might be ScribtableObjects which are one way to simply store data?
    [SerializeField] public ReactionStrategyImpl hunterReactionStrategy;
    [SerializeField] public ReactionStrategyImpl guardReactionStrategy;
    [SerializeField] public ReactionStrategyImpl patrolReactionStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl hunterNpcBehaviorStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl guardNpcBehaviorStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl patrolNpcBehaviorStrategy;
    
    [SerializeField] public int defence;
    [SerializeField] public int numberOfFigures;
    [SerializeField] public State state;
    [SerializeField] public List<string> hunterTargets;
    [SerializeField] public List<string> patrolTargets;
    [SerializeField] public List<string> guardTargets;

    // defined once, not changed:
    [SerializeField] public int figureEndurance;
    // Passive enemy properties which are good to add to a separate visible list for players to take account when eg. moving
    // data type not defined, we'll see how this goes
    [SerializeField] public string noticeableSkills;

    public Color colorInUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Reaction getRandomReaction() {
        switch (state) {
            case State.Hunter: return hunterReactionStrategy.getRandomReaction(getRandomElementFromList(hunterTargets));
            case State.Patrol: return patrolReactionStrategy.getRandomReaction(getRandomElementFromList(patrolTargets));
            case State.Guard: return guardReactionStrategy.getRandomReaction(getRandomElementFromList(guardTargets));
            default: return null;
        }
    }

    public void takeEffect() {

    }

    /*
    public string getRandomTargetBasedOnState()
    {
        switch(state) {
            case State.Hunter: return getRandomElementFromList(hunterTargets);
            case State.Patrol: return getRandomElementFromList(patrolTargets);
            case State.Guard: return getRandomElementFromList(guardTargets);
            default: return null;
        }
    }
    */

    private string getRandomElementFromList(List<string> list)
    {

        if (list != null && list.Count > 0)
        {
            return list[Random.Range(0, list.Count - 1)];
        } else
        {
            return null;
        }
    }

}
