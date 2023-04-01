using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolvedResult
{
    public string description;

    private List<Reaction> reactions;

    public ResolvedResult() {
        this.description = "";
        this.reactions = new List<Reaction>();
    }

    public void addToDescription(string desc) {
        description += desc + ", ";
    }

    public List<Reaction> setReactions(List<Reaction> reactions) {
        this.reactions = reactions;
        return this.reactions;
    }

    public List<Reaction> getReactions() {
        return reactions;
    }

    public List<Reaction> addToReactions(Reaction reaction) {
        reactions.Add(reaction);
        return reactions;
    }

    /**
    * Adds the contents of the param List to the existing Reactions list.
    * Each new reaction gets added only if it doesn't already exist in the existing List.
    */
    public List<Reaction> addToReactions(List<Reaction> newReactions) {
        foreach (Reaction newReaction in newReactions) {
            if (!reactions.Contains(newReaction)) {
                reactions.Add(newReaction);
            }
        }
        return reactions;
    }

}