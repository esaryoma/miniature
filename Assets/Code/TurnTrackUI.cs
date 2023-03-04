using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnTrackUI : MonoBehaviour
{
    public static TurnTrackUI turnTrackUI;
    public List<TurnUI> turnUIs;
    public GameObject turnUIprefab;
    public List<Character> turnOrder;

    public int roundCounter = 1;
    public int currentTurnIndex = 0;
    public bool actionDoneInCurrentTurn = false;

    void Awake()
    {
        turnTrackUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeRound()
    {
        roundCounter++;
        Control.control.roundCounterUI.text = "Round " + roundCounter.ToString();

        for (int i = 0; i < GetComponent<RectTransform>().childCount; i++)
        {
            Transform t = GetComponent<RectTransform>().GetChild(i);
            Destroy(t.gameObject);
        }

        turnOrder = new List<Character>();
        foreach (Character c in Control.control.players) 
        {
            turnOrder.Add(c);
        }
        foreach (Character c in Control.control.enemies)
        {
            turnOrder.Add(c);
        }

        for (int i = 0; i < turnOrder.Count; i++)
        {
            Character temp = turnOrder[i];
            int randomIndex = Random.Range(i, turnOrder.Count);
            turnOrder[i] = turnOrder[randomIndex];
            turnOrder[randomIndex] = temp;
        }

        for (int i = 0;i<turnOrder.Count;i++)
        {
            GameObject g = GameObject.Instantiate(turnUIprefab, GetComponent<RectTransform>());
            g.GetComponent<TurnUI>().Initialize(turnOrder[i]);
        } 
    }

    public void EndTurn()
    {
        currentTurnIndex++;
        StartCoroutine(InitializeTurn());
    }

    public IEnumerator InitializeTurn()
    {
        if (currentTurnIndex==turnOrder.Count)
        {
            currentTurnIndex = 0;
            InitializeRound();
        }

        Character c = turnOrder[currentTurnIndex];

        yield return new WaitForNextFrameUnit();

        transform.GetChild(currentTurnIndex).localScale = transform.GetChild(currentTurnIndex).localScale * 1.5f;

        float proportion = Control.control.skillCardUIPrefab.GetComponent<RectTransform>().rect.width / Control.control.skillCardUIPrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < Control.control.skillCardUIparent.childCount; i++)
        {
            Transform t = Control.control.skillCardUIparent.GetChild(i);
            Destroy(t.gameObject);
        }

        switch (c.characterType)
        {
            case Character.CharacterType.Player: 
                SelectPlayerCharacterPanel.selectPlayerCharacterPanel.gameObject.SetActive(true);
                SelectPlayerCharacterPanel.selectPlayerCharacterPanel.InitializeButtons();
                break;

            case Character.CharacterType.Enemy:
                transform.GetChild(currentTurnIndex).localScale = transform.GetChild(currentTurnIndex).localScale * 0.7f;
                transform.GetChild(currentTurnIndex).GetComponent<Image>().sprite = turnOrder[currentTurnIndex].characterSprite;
                break;
        }
        actionDoneInCurrentTurn = false;
        Control.control.BroadcastMessage("ChangeSkillColor",SendMessageOptions.DontRequireReceiver);

        Control.control.UpdateCurrentCharView();
    }

    public void InitializePlayerTurn(Player p)
    {
        for (int i = 0; i < p.skillCards.Count; i++)
        {
            Control.control.InitializeSkillCardUI(p, p.skillCards[i]);
        }
    }

}
