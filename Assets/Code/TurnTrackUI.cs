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
            GameObject g = GameObject.Instantiate(turnUIprefab, turnTrackUI.GetComponent<RectTransform>());

            switch ((turnOrder[i]).characterType)
                {

                case Character.CharacterType.Player:
                    g.GetComponent<Image>().color = Color.blue;
                    break;

                case Character.CharacterType.Enemy:
                    g.GetComponent<Image>().color = Color.red;
                    break;
            }
        }
    }

}
