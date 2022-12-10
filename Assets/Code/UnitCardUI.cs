using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitCardUI : MonoBehaviour, IPointerClickHandler
{

    public Character character;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (Control.control.selectedEnemies.Contains(character as Enemy))
        {
            Control.control.selectedEnemies.Remove(character as Enemy);
        }
        else
        {
            Control.control.selectedEnemies.Add(character as Enemy);
        }

        switch (Control.control.uiMode)
        {
            case Control.UImode.PlayerSkillCardCloseUp:
                Control.control.CheckIfReadyToConfirmSkills();
                break;
        } 
    }


}
