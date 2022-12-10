using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // how to switch between reaction/behaviorstrategies when enemy switces States (hunter, patrol, guard)?
    // reactions/behaviors might be ScribtableObjects which are one way to simply store data?
    [SerializeField] public ReactionStrategyImpl hunterReactionStrategy;
    [SerializeField] public ReactionStrategyImpl guardReactionStrategy;
    [SerializeField] public ReactionStrategyImpl patrolReactionStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl hunterNpcBehaviorStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl guardNpcBehaviorStrategy;
    [SerializeField] public NpcBehaviorStrategyImpl patrolNpcBehaviorStrategy;

    //public NpcBehaviorStrategy behaviorStrategy;
    [SerializeField] public int shieldPoints;
     
    public enum State
    {
        Hunter,
        Patrol,
        Guard
    }

    public State state;

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
            case State.Hunter: return hunterReactionStrategy.getRandomReaction();
            case State.Patrol: return patrolReactionStrategy.getRandomReaction();
            case State.Guard: return guardReactionStrategy.getRandomReaction();
            default: return null;
        }
    }

}
