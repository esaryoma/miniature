using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionStrategyImpl : MonoBehaviour, ReactionStrategy
{
    [SerializeField] public string reactionStrategyName;
    [SerializeField] public int awarenessRange;
    [SerializeField] public List<Reaction> reactions;

    /**
    * Return a random reaction listed in this ReactionStrategy
    * Returns a null if no reactions found
    */
    public Reaction getRandomReaction() {
        // randomly get this strategy's speficic reaction
        if (reactions != null && reactions.Count > 0) {
            int reactionIndex  = Random.Range(0, reactions.Count-1);
            return reactions[reactionIndex];
        } 
        return null;
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
