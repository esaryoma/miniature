using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction
{
    public Character player;
    public Skill skill;
    public List<Character> targets;

    public List<Effect> getSkillEffects() {
        return skill.effects;
    }
}