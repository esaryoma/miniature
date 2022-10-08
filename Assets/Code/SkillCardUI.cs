using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillCardUI : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI SkillCardName;
    public TextMeshProUGUI baseSkillText;
    public List<RectTransform> skillUIs;
    public RectTransform skillsParent;
    public SkillCard skillCard;

    public List<SelectableSkillUI> selectableSkillUIs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseCloseUp()
    {
        for (int i = 0; i < selectableSkillUIs.Count; i++)
        {
            if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.Selected || selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.DisabledAndBought)
            {
                selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.NotSelected);
                selectableSkillUIs[i].gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true;
                selectableSkillUIs[i].GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                Control.control.players[Control.control.currentCharacterIndex].resolve = Control.control.players[Control.control.currentCharacterIndex].resolve + selectableSkillUIs[i].resolvePrice;
            }

            if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.Disabled)
            {
                selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.NotSelected);
            }
        }
        Control.control.UpdateCurrentCharView();
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        Control.control.InitializeCardCloseUp(this);
    }

    public void CheckSkillAvailabilityInCloseUp()
    {
        for (int i = 0;i<selectableSkillUIs.Count;i++)
        {
            if (selectableSkillUIs[i].resolvePrice > Control.control.players[Control.control.currentCharacterIndex].resolve)
            {
                if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.NotSelected)
                {
                    selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.Disabled);
                }

                if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.Selected)
                {
                    selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.DisabledAndBought);
                }
            }
            else
            {
                if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.Disabled)
                {
                    selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.NotSelected);
                }

                if (selectableSkillUIs[i].skillState == SelectableSkillUI.SkillState.DisabledAndBought)
                {
                    selectableSkillUIs[i].SetState(SelectableSkillUI.SkillState.Selected);
                }
            }
        }
    }

}
