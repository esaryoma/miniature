using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction
{
    public Character player;
    public Skill skill;
    public List<Character> targets;

    public PlayerAction(Skill targetSkill, List<Character> targetList)
    {
        targets = targetList;
        skill = targetSkill;
    }

    public List<Effect> getSkillEffects() {
        return skill.effects;
    }
}