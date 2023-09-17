using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Effect : MonoBehaviour
{
    [SerializeField] public string effectName;
    [SerializeField] public int damage;
    [SerializeField] public int areaOfEffect;
    [SerializeField] public int playerMove;
    [SerializeField] public int targetMove;
    [SerializeField] public int wound;
    [SerializeField] public int range;
    [SerializeField] public Status status;
    [SerializeField] public int pierceArmor;
    [SerializeField] public string freeText;

    public enum TargetType
    {
        Friendly,
        Enemy,
        Self,
        Any
    }

    public TargetType targetType;

    public enum EffectType
    {
        // skillName, damage, areaOfEffect, wound, range, status.statusType, status.length, pierceArmor
        Damage,

        // targetMove, range
        TargetMove,

        // playerMove, range
        PlayerMove,

        // status
        StatusOnly
    }

    public EffectType effectType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
