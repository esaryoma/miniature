using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerCharacterInitializer))]
public class PlayerCharacterInitializerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerCharacterInitializer myTarget = (PlayerCharacterInitializer)target;
       
        if (GUILayout.Button("InitializeCharacters"))
        {
            Clear();
            Character[] characters = myTarget.transform.GetComponentsInChildren<Character>();

            foreach (Character c in characters)
            {
                SkillCard[] skillCards = c.transform.GetComponentsInChildren<SkillCard>();

                foreach (SkillCard s in skillCards)
                {
                    Skill[] skills = s.transform.GetComponentsInChildren<Skill>();
                    
                    switch (c.characterType)
                    {
                        case Character.CharacterType.Player:
                            Player p =  c as Player;
                            p.skillCards.Add(s);
                            break;
                    }

                    foreach (Skill ss in skills)
                    {
                        Effect[] effects = ss.transform.GetComponentsInChildren<Effect>();
                        s.skills.Add(ss);

                        foreach (Effect e in effects)
                        {
                            ss.effects.Add(e);
                        }
                    }
                }
            }
        }

    }

    void Clear()
    {
        PlayerCharacterInitializer myTarget = (PlayerCharacterInitializer)target;
        Character[] characters = myTarget.transform.GetComponentsInChildren<Character>();

        foreach (Character c in characters)
        {
            SkillCard[] skillCards = c.transform.GetComponentsInChildren<SkillCard>();

            foreach (SkillCard s in skillCards)
            {
                Skill[] skills = s.transform.GetComponentsInChildren<Skill>();

                foreach (Skill ss in skills)
                {
                    ss.effects.Clear();
                }

                s.skills.Clear();
            }

            switch (c.characterType)
            {
                case Character.CharacterType.Player:
                    Player p = c as Player;
                    p.skillCards.Clear();
                    break;
            }

        }
    }
}
