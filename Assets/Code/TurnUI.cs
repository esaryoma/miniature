using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{

    public Sprite[] icons;
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Character c)
    {
        character = c;
        Image image = GetComponent<Image>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.y, GetComponent<RectTransform>().sizeDelta.y);

        switch (character.characterType)
        {

            case Character.CharacterType.Player:
                image.sprite = icons[0];
                break;

            case Character.CharacterType.Enemy:
                image.sprite = icons[1];
                break;
        }
    }
}
