using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviorStrategyImpl : MonoBehaviour, NpcBehaviorStrategy
{
    public List<NpcBehavior> npcBehaviors;

    public NpcBehavior getRandomNpcBehavior() {
        // randomly get this strategy's speficic behavior
        
        if (npcBehaviors != null && npcBehaviors.Count > 0) {
            int behaviorIndex  = Random.Range(0, npcBehaviors.Count-1);
            return npcBehaviors[behaviorIndex];
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
