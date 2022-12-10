using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    public static Control control; 
    public List<float> slot1probabilities;
    public List<float> slot2probabilities;
    public List<float> slot3probabilities;
    public List<float> slot4probabilities;
    public List<float> slot5probabilities;

    public List<Sprite> propabilitySprites;
    public List<Sprite> skillPriceSprites;

    public int currentCharacterIndex = 0;

    public enum UImode
    {
        PlayerTurn,
        PlayerSkillCardCloseUp
    }

    public UImode uiMode = UImode.PlayerTurn;

    public List<Player> players;
    public List<Enemy> enemies;

    [SerializeField] GameObject skillCardUIPrefab;
    [SerializeField] GameObject skillUIPrefab;
    [SerializeField] GameObject unitCardUIPrefab;
    public RectTransform skillCardUIparent;
    public RectTransform unitCardUIparent;

    public SkillCardUI skillCardUIcloseUp;
    public SkillCard skillCardInCloseUp;
    public GameObject bgDimmer;

    public TextMeshProUGUI currentPlayerNameUItext;
    public TextMeshProUGUI currentPlayerEnduranceUItext;
    public TextMeshProUGUI currentPlayerResolveUItext;
    public TextMeshProUGUI currentPlayerWoundsUItext;

    // Start is called before the first frame update
    void Awake()
    {
        control = this;
        InitializePlayerTurn(players[0]);
        InitializeEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void UpdateCurrentCharView()
    {
        currentPlayerNameUItext.text = players[currentCharacterIndex].charName;
        currentPlayerEnduranceUItext.text = players[currentCharacterIndex].endurance.ToString();
        currentPlayerResolveUItext.text = players[currentCharacterIndex].resolve.ToString();
        currentPlayerWoundsUItext.text = players[currentCharacterIndex].wounds.ToString();
    }

    public void CloseCardCloseUp()
    {
        if (Control.control.uiMode == Control.UImode.PlayerSkillCardCloseUp)
        {
            Control.control.uiMode = Control.UImode.PlayerTurn;
            skillCardUIcloseUp.CloseCloseUp();
            bgDimmer.SetActive(false);
            skillCardInCloseUp = null;
        }
    }
    public void InitializeCardCloseUp(SkillCardUI skillCardUI, int siblingIndex)
    {
        Control.control.skillCardUIcloseUp.gameObject.SetActive(true);
        bgDimmer.SetActive(true);
        InitializeSkillCardUI(players[0], skillCardUI.skillCard, true);
        uiMode = UImode.PlayerSkillCardCloseUp;
        InitializePropabilityImages(skillCardUI, siblingIndex);
    }

    public void InitializeEnemies()
    {
    foreach (Enemy e in enemies)
        {
            GameObject g = GameObject.Instantiate(unitCardUIPrefab, unitCardUIparent);           
        }
    }

    void InitializePropabilityImages(SkillCardUI skillCardUI, int siblingIndex)
    {
        int i = siblingIndex;
        Debug.Log(i);
        List<float> probs = new List<float>();

        switch (i)
        {
            case 0:
                probs = slot1probabilities;
                break;
            case 1:
                probs = slot2probabilities;
                break;
            case 2:
                probs = slot3probabilities;
                break;
            case 3:
                probs = slot4probabilities;
                break;
            case 4:
                probs = slot5probabilities;
                break;
        }

        for (int ii = 0; ii < 3; ii++)
        {
            SkillUI s = skillCardUIcloseUp.skillUIs[ii].GetComponent<SkillUI>();
            s.propabilityImage.sprite = propabilitySprites[3];
            if (probs[ii] > 25f) { s.propabilityImage.sprite = propabilitySprites[0]; }
            if (probs[ii] > 50f) { s.propabilityImage.sprite = propabilitySprites[1]; }
            if (probs[ii] > 75f) { s.propabilityImage.sprite = propabilitySprites[2]; }
        }
    }

    void InitializePlayerTurn(Player player)
    {
        float proportion = skillCardUIPrefab.GetComponent<RectTransform>().rect.width / skillCardUIPrefab.GetComponent<RectTransform>().rect.height;

        UpdateCurrentCharView();

        for (int i = 0; i < player.skillCards.Count; i++)
        {
            InitializeSkillCardUI(player, player.skillCards[i]);
        }
        }

    void InitializeSkillCardUI(Player player,SkillCard skillCard, bool closeUp = false)
    {
        GameObject g;
        RectTransform t;

        if (!closeUp)
        {
            g = GameObject.Instantiate(skillCardUIPrefab, skillCardUIparent);
            t = g.GetComponent<RectTransform>();
        }
        else
        {
            g = skillCardUIcloseUp.gameObject; 
            t = g.GetComponent<RectTransform>();
        }

        SkillCardUI skillCardUI = t.GetComponent<SkillCardUI>();
        skillCardUI.skillCard = skillCard;
        skillCardUI.SkillCardName.text = skillCard.skillCardName;
        skillCardUI.baseSkillText.text = ParseSkillText(skillCard.skills[0]);
         

        for (int ii = 1; ii < skillCard.skills.Count; ii++)
        {
            SkillUI s = skillCardUI.skillUIs[ii - 1].GetComponent<SkillUI>();
            s.skillNameUItext.text = skillCard.skills[ii].name;
            s.skillUItext.text = ParseSkillText(skillCard.skills[ii]);
            s.skill = skillCard.skills[ii];

            if (!closeUp)
            {
                s.enabled = false;
            }
        } 

        skillCardInCloseUp = skillCard;
    }


    string ParseSkillText(Skill s)
    {
        string parsed = "";

        if (s.damage != 0) { parsed = parsed + "Damage " + s.damage.ToString() + ", "; }
        if (s.wound != 0) { parsed = parsed + "Wound " + s.wound.ToString() + ", "; }
        if (s.areaOfEffect != 0) { parsed = parsed + "Area " + s.areaOfEffect.ToString() + ", "; }
        if (s.playerMove != 0) { parsed = parsed + "Move " + s.playerMove.ToString() + ", "; }
        if (s.targetMove != 0) { parsed = parsed + "Push/Pull " + s.targetMove.ToString() + ", "; }
        if (s.range != 0) { parsed = parsed + "Range " + s.range.ToString() + ", "; }
        if (s.status != null) { parsed = parsed + s.status.statusType + " " + s.status.length + ", "; }
        if (s.pierceArmor != 0) { parsed = parsed + "Pierce " + s.pierceArmor + ", "; }
        if (s.freeText != "") { parsed = parsed + s.freeText + ", "; }

        parsed = parsed.Substring(0, parsed.Length - 2);

        return parsed;
    }

}
