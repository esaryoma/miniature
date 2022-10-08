using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableSkillUI : MonoBehaviour, IPointerClickHandler
{

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

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (skillState == SkillState.NotSelected)
        {
            SetState(SkillState.Selected); 
            GetComponent<Image>().color = new Color(Color.magenta.r, Color.magenta.g, Color.magenta.b, 0.5f);
            Control.control.players[Control.control.currentCharacterIndex].resolve = Control.control.players[Control.control.currentCharacterIndex].resolve - resolvePrice;
            Control.control.UpdateCurrentCharView();
            skillCardUI.CheckSkillAvailabilityInCloseUp();
            gameObject.GetComponent<SkillUI>().propabilityImage.enabled = false;
            return;
        }

        if (skillState == SkillState.Selected)
        {
            SetState(SkillState.NotSelected);
            gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true; 
            GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
            Control.control.players[Control.control.currentCharacterIndex].resolve = Control.control.players[Control.control.currentCharacterIndex].resolve + resolvePrice;
            Control.control.UpdateCurrentCharView();
            skillCardUI.CheckSkillAvailabilityInCloseUp();
            return;
        }

        if (skillState == SkillState.DisabledAndBought)
        {
            SetState(SkillState.NotSelected);
            gameObject.GetComponent<SkillUI>().propabilityImage.enabled = true;
            GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
            Control.control.players[Control.control.currentCharacterIndex].resolve = Control.control.players[Control.control.currentCharacterIndex].resolve + resolvePrice;
            Control.control.UpdateCurrentCharView();
            skillCardUI.CheckSkillAvailabilityInCloseUp();
            return;
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
