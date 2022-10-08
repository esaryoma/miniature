using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    public static Control control;
    public float damage;

    public List<float> slot1probabilities;
    public List<float> slot2probabilities;
    public List<float> slot3probabilities;
    public List<float> slot4probabilities;
    public List<float> slot5probabilities;

    public enum UImode
    {
        PlayerTurn,
        PlayerSkillCardCloseUp
    }

    public UImode uiMode = UImode.PlayerTurn;

    public List<Player> players;

    [SerializeField] GameObject skillCardUIPrefab;
    [SerializeField] GameObject skillUIPrefab;
    public RectTransform skillCardUIparent;

    public SkillCardUI skillCardUIcloseUp;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseCardCloseUp()
    {
        if (Control.control.uiMode == Control.UImode.PlayerSkillCardCloseUp)
        {
            Control.control.uiMode = Control.UImode.PlayerTurn;
            skillCardUIcloseUp.gameObject.SetActive(false);
            bgDimmer.SetActive(false);
        }
    }
    public void InitializeCardCloseUp(SkillCard skillCard)
    {
        Control.control.skillCardUIcloseUp.gameObject.SetActive(true);
        bgDimmer.SetActive(true);
        InitializeSkillCardUI(players[0], skillCard, true);
        uiMode = UImode.PlayerSkillCardCloseUp;
    }

    void InitializePlayerTurn(Player player)
    {
        float proportion = skillCardUIPrefab.GetComponent<RectTransform>().rect.width / skillCardUIPrefab.GetComponent<RectTransform>().rect.height;

        currentPlayerNameUItext.text = player.charName;
        currentPlayerEnduranceUItext.text = player.endurance.ToString();
        currentPlayerResolveUItext.text = player.resolve.ToString();
        currentPlayerWoundsUItext.text = player.wounds.ToString();

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
        }
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
