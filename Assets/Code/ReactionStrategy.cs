using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ReactionStrategy
{
    Reaction getRandomReaction(string preferredTarget);
}
