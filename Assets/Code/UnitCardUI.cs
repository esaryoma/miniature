using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; 

public class UnitCardUI : MonoBehaviour, IPointerClickHandler
{

    public Enemy enemy;
    public Image cardBG;
    public Image charImage;
    public Image figureImage;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI raceField;
    public TextMeshProUGUI stateField;
    public TextMeshProUGUI classField;
    public TextMeshProUGUI numberField;
    public TextMeshProUGUI woundsField;
    public TextMeshProUGUI enduranceField;
    public TextMeshProUGUI resolveField;
    public TextMeshProUGUI shieldsField;

    // Start is called before the first frame update
    void Start()
    {
        cardBG.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCard()
    {
        charImage.sprite = enemy.characterSprite;
        figureImage.sprite = enemy.figureSprite; 
        nameField.text = enemy.charName;
        raceField.text = enemy.characterRace.ToString();
        stateField.text = enemy.state.ToString();
        classField.text = enemy.characterClass.ToString();

        if (enemy.numberOfFigures > 1)
        {
            numberField.text = "Horde " + enemy.numberOfFigures.ToString();
        }
        else
        {
            numberField.text = "Single";
        } 
        woundsField.text = enemy.wounds.ToString();
        enduranceField.text = enemy.endurance.ToString();
        resolveField.text = enemy.resolve.ToString();
        shieldsField.text = "Shields " + enemy.shieldPoints.ToString();
}

    public void OnPointerClick(PointerEventData eventData) // 3
    {


        switch (Control.control.uiMode)
        {
            case Control.UImode.PlayerSkillCardCloseUp:


                if (Control.control.selectedEnemies.Contains(enemy))
                {
                    Control.control.selectedEnemies.Remove(enemy);
                    cardBG.color = Color.gray;
                }
                else
                {
                    Control.control.selectedEnemies.Add(enemy);
                    cardBG.color = Color.white;
                }


                Control.control.CheckIfReadyToConfirmSkills();
                break;

            case Control.UImode.PlayerTurn:
                Control.control.InitializeUnitCardCloseUp(enemy);
                break;
        } 
    }


}
