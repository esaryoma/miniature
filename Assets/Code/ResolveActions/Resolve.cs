using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Resolve
{
    public static ResolvedResult resolve(List<PlayerAction> actions) {

        Dictionary<Character, int> dmgMap = new Dictionary<Character, int>();

        ResolvedResult result = new ResolvedResult();
        Debug.Log("Actions received to resolve: " + actions.Count);
        foreach(PlayerAction action in actions) {
            Debug.Log("Resolving action: " + action.skill.skillName);
            foreach(Effect effect in action.getSkillEffects()) {
                switch(effect.effectType) 
                    {
                    case Effect.EffectType.Damage:
                        Debug.Log("Resolving effect type: damage...");
                        addToDmg(effect, action.targets, dmgMap);
                        addToStatus(effect.status, action.targets, result);
                        break;
                    case Effect.EffectType.PlayerMove:
                        // code block
                        break;
                    case Effect.EffectType.TargetMove:
                        // code block
                        break;
                    case Effect.EffectType.StatusOnly:
                        addToStatus(effect.status, action.targets, result);
                        break;
                    default:
                        // code block
                        break;
                    }
            }
        }

        reduceDmgByTarget(dmgMap, result);
        
        foreach(Character target in dmgMap.Keys) {
            addDmgToResult(dmgMap[target], target, result);
            inflictDmg(dmgMap[target], target, result);
        }
        return result;
    }

    private static void inflictDmg(int inflictedDmg, Character target, ResolvedResult result) {
        target.endurance -= inflictedDmg;
        if (target.endurance <= 0) {
            result.addToDescription("Remove character/unit " + target.charName + " from play.");
        }
    }

    private static void reduceDmgByTarget(Dictionary<Character,int> dmgMap, ResolvedResult result) {
        
        var dmgMapCopy = dmgMap.ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (Character target in dmgMapCopy.Keys) {
            int dmgLeftOver = dmgMap[target];
            if (target.characterType == Character.CharacterType.Enemy) {
                dmgLeftOver = reduceDmgByEnemyTarget(dmgMap[target], target, result);
            } else if (target.characterType == Character.CharacterType.Player) {
                dmgLeftOver = reduceDmgByPlayerTarget(dmgMap[target], target, result);
            }
            dmgMap[target] = dmgLeftOver;
        }
    }

    private static int reduceDmgByPlayerTarget(int inflictedDmg, Character target, ResolvedResult result) {
        // TODO
        return 0;
    }

    private static int reduceDmgByEnemyTarget(int inflictedDmg, Character target, ResolvedResult result) {
        Enemy enemy = target as Enemy;
        int dmgLeftOver = inflictedDmg;
        
        if (enemy.defence > 0) {
            dmgLeftOver -= enemy.defence;
            result.addToDescription(target.charName + " defended for " + enemy.defence);
        }

        if (dmgLeftOver > 0 && enemy.resolve > 0) {
            dmgLeftOver -= enemy.resolve;
            // remove enemy resolve for the correct amount
            if (dmgLeftOver <= 0) {
                result.addToDescription(target.charName + " shielded with " + inflictedDmg + " resolve points.");
                enemy.resolve -= inflictedDmg;
                // no damage to actually inflict
                return 0;
            } else if (dmgLeftOver > 0) {
                result.addToDescription(target.charName + " shielded with " + enemy.resolve + " resolve points.");
                enemy.resolve = 0;
            }
        }

        
        // TODO other damage negation...
        return dmgLeftOver;
    }

    private static void addToDmg(Effect effect, List<Character> targets, Dictionary<Character, int> dmgMap) {
        foreach (Character target in targets) {
            if (dmgMap.ContainsKey(target)) {
                dmgMap[target] += effect.damage;
            } else {
                dmgMap[target] = effect.damage;
            }
        }
    }

    private static void addToStatus(Status status, List<Character> targets, ResolvedResult result) {
        if (status != null) {
            foreach(Character target in targets) {
                if (target.getImmunities() != null && target.getImmunities().Contains(status)) {
                    Debug.Log("Character " + target.charName + " was immune to " + status.statusType);
                    addImmunityToDescription(status, target, result);
                } else 
                    Debug.Log("Adding effect: " + status.statusType);
                    target.addToStatus(status);
                    addStatusToDescription(status, target, result);
            }
        }
    }

    private static void addDmgToResult(int inflictedDmg, Character target, ResolvedResult result) {
        result.addToDescription("Inflicted " + inflictedDmg + " to " + target.charName);
    }

    private static void addStatusToDescription(Status status, Character target, ResolvedResult result) {
        result.addToDescription("Added status " + status.statusType + " to " + target.charName);
    }

    private static void addImmunityToDescription(Status status, Character target, ResolvedResult result) {
        result.addToDescription(target.charName + " is immune to " + status.statusType + "!");
    }

}
