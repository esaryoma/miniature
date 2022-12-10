using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitCardUI : MonoBehaviour, IPointerClickHandler
{

    public Character character;
    public Image cardBG;

    // Start is called before the first frame update
    void Start()
    {
        cardBG.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {


        switch (Control.control.uiMode)
        {
            case Control.UImode.PlayerSkillCardCloseUp:


                if (Control.control.selectedEnemies.Contains(character as Enemy))
                {
                    Control.control.selectedEnemies.Remove(character as Enemy);
                    cardBG.color = Color.gray;
                }
                else
                {
                    Control.control.selectedEnemies.Add(character as Enemy);
                    cardBG.color = Color.white;
                }


                Control.control.CheckIfReadyToConfirmSkills();
                break;
        } 
    }


}
