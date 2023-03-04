using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Xml;

public class SkillCardUI : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI SkillCardName;
    public TextMeshProUGUI baseSkillText;
    public List<SkillUI> skillUIs;
    public RectTransform skillsParent;
    public SkillCard skillCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Skill> ReturnSelectedSkills()
    {
        List<Skill> skillsSelected = new List<Skill>();     

        skillsSelected.Add(skillCard.skills[0]); //ALWAYS ADD BASIC SKILL (INDEX 0)

        for (int i=1;i<skillUIs.Count;i++)
        {
            if (skillUIs[i].gameObject.activeSelf && skillUIs[i].skillState == SkillUI.SkillState.Selected)
            {
                skillsSelected.Add(skillUIs[i].GetComponent<SkillUI>().skill);
            }
        }

        return skillsSelected;
    }

    public void CloseCloseUp(bool skillsResolved)
    {
        for (int i = 0; i < skillUIs.Count; i++)
        {
            if (skillUIs[i].skillState == SkillUI.SkillState.Selected || skillUIs[i].skillState == SkillUI.SkillState.DisabledAndBought)
            {
                skillUIs[i].SetState(SkillUI.SkillState.NotSelected);
                skillUIs[i].gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true;
                if (!skillsResolved) 
                { 
                    Control.control.players[Control.control.currentPlayerCharacterIndex].resolve = Control.control.players[Control.control.currentPlayerCharacterIndex].resolve + skillUIs[i].resolvePrice;
                    skillUIs[i].GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                }
            }

            if (skillUIs[i].skillState == SkillUI.SkillState.Disabled)
            {
                skillUIs[i].SetState(SkillUI.SkillState.NotSelected);
            }
        }
        Control.control.UpdateCurrentCharView();
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (Control.control.uiMode == Control.UImode.PlayerTurn) { Control.control.InitializeCardCloseUp(this, transform.GetSiblingIndex()); }
    }

    public void CheckSkillAvailabilityInCloseUp()
    {
        for (int i = 0;i<skillUIs.Count;i++)
        {
            if (skillUIs[i].resolvePrice > Control.control.players[Control.control.currentPlayerCharacterIndex].resolve)
            {
                if (skillUIs[i].skillState == SkillUI.SkillState.NotSelected)
                {
                    skillUIs[i].SetState(SkillUI.SkillState.Disabled);
                }

                if (skillUIs[i].skillState == SkillUI.SkillState.Selected)
                {
                    skillUIs[i].SetState(SkillUI.SkillState.DisabledAndBought);
                }
            }
            else
            {
                if (skillUIs[i].skillState == SkillUI.SkillState.Disabled)
                {
                    skillUIs[i].SetState(SkillUI.SkillState.NotSelected);
                }

                if (skillUIs[i].skillState == SkillUI.SkillState.DisabledAndBought)
                {
                    skillUIs[i].SetState(SkillUI.SkillState.Selected);
                }
            }
        }
    }

}
