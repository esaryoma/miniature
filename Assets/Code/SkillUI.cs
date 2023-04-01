using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI skillNameUItext;
    public TextMeshProUGUI skillUItext;
    public Image propabilityImage;
    public Skill skill;


    public SkillCardUI skillCardUI;
    public int resolvePrice;
    public Image priceImage;

    public enum SkillState
    {
        NotSelected,
        Selected,
        Disabled,
        DisabledAndBought
    }

    public SkillState skillState = SkillState.NotSelected;

    // Start is called before the first frame update
    void Start()
    {

    }

    void ChangeSkillColor()
    { 
        if (TurnTrackUI.turnTrackUI.actionDoneInCurrentTurn == true)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0.39f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 0, 0, 0.39f);
        }
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (!TurnTrackUI.turnTrackUI.actionDoneInCurrentTurn)
        {
            if (skillState == SkillState.NotSelected)
            {
                SetState(SkillState.Selected);
                GetComponent<Image>().color = new Color(Color.magenta.r, Color.magenta.g, Color.magenta.b, 0.5f); 
                Control.control.players[Control.control.currentPlayerCharacterIndex].resolve = Control.control.players[Control.control.currentPlayerCharacterIndex].resolve - resolvePrice;
                Control.control.UpdateCurrentCharView();
                skillCardUI.CheckSkillAvailabilityInCloseUp();
                gameObject.GetComponent<SkillUI>().propabilityImage.enabled = false;
                Control.control.CheckIfReadyToConfirmSkills();
                return;
            }

            if (skillState == SkillState.Selected)
            {
                SetState(SkillState.NotSelected);
                gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true;
                GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                Control.control.players[Control.control.currentPlayerCharacterIndex].resolve = Control.control.players[Control.control.currentPlayerCharacterIndex].resolve + resolvePrice;
                Control.control.UpdateCurrentCharView();
                skillCardUI.CheckSkillAvailabilityInCloseUp();
                Control.control.CheckIfReadyToConfirmSkills();
                return;
            }

            if (skillState == SkillState.DisabledAndBought)
            {
                SetState(SkillState.NotSelected);
                gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true;
                GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                Control.control.players[Control.control.currentPlayerCharacterIndex].resolve = Control.control.players[Control.control.currentPlayerCharacterIndex].resolve + resolvePrice;
                Control.control.UpdateCurrentCharView();
                skillCardUI.CheckSkillAvailabilityInCloseUp();
                Control.control.CheckIfReadyToConfirmSkills();
                return;
            }
        }
      
    }

    public void SetState(SkillState skillStateToSet)
    {
        if (skillStateToSet == SkillState.Disabled)
        {
            if (skillState == SkillState.NotSelected) { skillState = SkillState.Disabled; }
            priceImage.sprite = Control.control.skillPriceSprites[1];
            priceImage.gameObject.SetActive(true);
        }

        if (skillStateToSet == SkillState.DisabledAndBought)
        {
            skillState = SkillState.DisabledAndBought;
            priceImage.sprite = Control.control.skillPriceSprites[1];
            priceImage.gameObject.SetActive(false);
        }

        if (skillStateToSet == SkillState.Selected)
        {
            skillState = SkillState.Selected;
            priceImage.sprite = Control.control.skillPriceSprites[0];
            priceImage.gameObject.SetActive(false);
        }

        if (skillStateToSet == SkillState.NotSelected)
        {
            skillState = SkillState.NotSelected;
            priceImage.sprite = Control.control.skillPriceSprites[0];
            priceImage.gameObject.SetActive(true);
        }
    }

}
