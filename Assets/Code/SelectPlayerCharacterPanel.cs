using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPlayerCharacterPanel : MonoBehaviour
{
    public static SelectPlayerCharacterPanel selectPlayerCharacterPanel;

    public Button[] buttons;
    public Image[] buttonImages;

    // Start is called before the first frame update
    void Start()
    {
        selectPlayerCharacterPanel = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeButtons()
    {
        for (int i = 0;i<buttons.Length;i++)
        {
            if (i<Control.control.players.Count)
            {
                 
                buttons[i].gameObject.SetActive(true);
                buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Control.control.players[i].charName;
                buttonImages[i].sprite = Control.control.players[i].characterSprite;
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    public void ButtonPressed(int n)
    {
        TurnTrackUI.turnTrackUI.InitializePlayerTurn(n);
        gameObject.SetActive(false);
    }
}
