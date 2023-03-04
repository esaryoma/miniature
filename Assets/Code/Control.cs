using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

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

    public int currentPlayerCharacterIndex = 0;

    public Transform canvas;

    public enum UImode
    {
        PlayerTurn,
        PlayerSkillCardCloseUp
    }

    public UImode uiMode = UImode.PlayerTurn;

    public List<Player> players;
    public List<Enemy> enemies;
    public List<Enemy> selectedEnemies;

    public GameObject skillCardUIPrefab;
    [SerializeField] GameObject skillUIPrefab;
    [SerializeField] GameObject unitCardUIPrefab;
    public RectTransform skillCardUIparent;
    public RectTransform unitCardUIparent;

    public SkillCardUI skillCardUIcloseUp;
    public SkillCard skillCardInCloseUp;
    public GameObject bgDimmer;
    public SkillCardUI selectedCardUI;

    public TextMeshProUGUI currentPlayerNameUItext;
    public TextMeshProUGUI currentPlayerEnduranceUItext;
    public TextMeshProUGUI currentPlayerResolveUItext;
    public TextMeshProUGUI currentPlayerWoundsUItext;

    public TextMeshProUGUI roundCounterUI;

    public Button confirmCharacterSelectionButton;
    public TextMeshProUGUI skillConfirmButtonText;
    public SkillUseSummaryUI skillUseSummaryUI;
    public SkillUseReactionUI skillUseReactionUI;
    int skillConfirmButtonState = 0;
    int resolveToBeAdded = 0;

    public TextMeshProUGUI resolvePromptText;

    // Start is called before the first frame update
    void Awake()
    {
        control = this;
    }

    private void Start()
    {

        InitializeEnemies();
        TurnTrackUI.turnTrackUI.EndTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void UpdateCurrentCharView()
    {
        currentPlayerNameUItext.text = TurnTrackUI.turnTrackUI.turnOrder[TurnTrackUI.turnTrackUI.currentTurnIndex].charName.ToString();
        currentPlayerEnduranceUItext.text = TurnTrackUI.turnTrackUI.turnOrder[TurnTrackUI.turnTrackUI.currentTurnIndex].endurance.ToString();
        currentPlayerResolveUItext.text = TurnTrackUI.turnTrackUI.turnOrder[TurnTrackUI.turnTrackUI.currentTurnIndex].resolve.ToString();
        currentPlayerWoundsUItext.text = TurnTrackUI.turnTrackUI.turnOrder[TurnTrackUI.turnTrackUI.currentTurnIndex].wounds.ToString();
    }

    public void CloseCardCloseUp(bool skillsResolved = false)
    {
        if (Control.control.uiMode == Control.UImode.PlayerSkillCardCloseUp)
        {
            Control.control.uiMode = Control.UImode.PlayerTurn;
            skillCardUIcloseUp.CloseCloseUp(skillsResolved);
            bgDimmer.SetActive(false);
            skillCardInCloseUp = null;
            confirmCharacterSelectionButton.gameObject.SetActive(false);

            for (int i = 0; i < unitCardUIparent.childCount; i++)
            {
                Transform t = unitCardUIparent.GetChild(i);
                t.GetComponent<UnitCardUI>().cardBG.color = Color.gray;
            }

            selectedEnemies.Clear();

            skillUseSummaryUI.gameObject.SetActive(false);
            skillUseReactionUI.gameObject.SetActive(false);
            resolvePromptText.gameObject.SetActive(false);

        }
    }
    public void InitializeCardCloseUp(SkillCardUI skillCardUI, int siblingIndex)
    {
        selectedCardUI = skillCardUI;
        Control.control.skillCardUIcloseUp.gameObject.SetActive(true);
        if (!TurnTrackUI.turnTrackUI.actionDoneInCurrentTurn) { Control.control.skillCardUIcloseUp.BroadcastMessage("ChangeSkillColor", SendMessageOptions.DontRequireReceiver); }
        bgDimmer.SetActive(true);
        InitializeSkillCardUI(players[0], skillCardUI.skillCard, true);
        uiMode = UImode.PlayerSkillCardCloseUp;
        InitializePropabilityImages(skillCardUI, siblingIndex);
        resolvePromptText.text = "";
    }

    public void InitializeEnemies()
    {
    foreach (Enemy e in enemies)
        {
            GameObject g = GameObject.Instantiate(unitCardUIPrefab, unitCardUIparent);
            g.GetComponent<UnitCardUI>().character = e;
            g.GetComponent<UnitCardUI>().charImage.sprite = e.characterSprite;
            g.GetComponent<Image>().color = e.colorInUI;
        }
    }

    void InitializePropabilityImages(SkillCardUI skillCardUI, int siblingIndex)
    {
        int i = siblingIndex; 
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

    

    public void InitializeSkillCardUI(Player player,SkillCard skillCard, bool closeUp = false)
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
            s.resolvePrice = ii;

            if (!closeUp)
            {
                s.enabled = false;
            }
        } 

        skillCardInCloseUp = skillCard;
    }


    string ParseSkillText(Skill skill)
    {
        string parsed = "";

        foreach (Effect effect in skill.effects)
        {
            if (effect.damage != 0) { parsed = parsed + "Damage " + effect.damage.ToString() + ", "; }
            if (effect.wound != 0) { parsed = parsed + "Wound " + effect.wound.ToString() + ", "; }
            if (effect.areaOfEffect != 0) { parsed = parsed + "Area " + effect.areaOfEffect.ToString() + ", "; }
            if (effect.playerMove != 0) { parsed = parsed + "Move " + effect.playerMove.ToString() + ", "; }
            if (effect.targetMove != 0) { parsed = parsed + "Push/Pull " + effect.targetMove.ToString() + ", "; }
            if (effect.range != 0) { parsed = parsed + "Range " + effect.range.ToString() + ", "; }
            if (effect.status != null) { parsed = parsed + effect.status.statusType + " " + effect.status.length + ", "; }
            if (effect.pierceArmor != 0) { parsed = parsed + "Pierce " + effect.pierceArmor + ", "; }
            if (effect.freeText != "") { parsed = parsed + effect.freeText + ", "; }

        }

        if (parsed.Length > 2)
        {
            parsed = parsed.Substring(0, parsed.Length - 2);
        }

        return parsed;
    }

    public void ConfirmSkillUseButtonPressed()
    {
    

    switch (skillConfirmButtonState)
        {
            case 0:
            TurnTrackUI.turnTrackUI.actionDoneInCurrentTurn = true;

            resolveToBeAdded = (selectedCardUI.transform.GetSiblingIndex() + 1);
            resolvePromptText.text = selectedCardUI.SkillCardName.text + " moved to first slot, you will gain " + (selectedCardUI.transform.GetSiblingIndex() + 1).ToString() + " resolve";
            selectedCardUI.transform.SetAsFirstSibling();

            resolvePromptText.gameObject.SetActive(true);

            canvas.BroadcastMessage("ChangeSkillColor");

            skillConfirmButtonText.text = "End";
            skillConfirmButtonState = 1;

            skillUseSummaryUI.gameObject.SetActive(true);
            skillUseReactionUI.gameObject.SetActive(true);

            List<PlayerAction> playerActions = new List<PlayerAction>();

            List<Skill> skills = skillCardUIcloseUp.ReturnSelectedSkills();

            List<Character> targets = new List<Character>();
            foreach (Enemy e in selectedEnemies)
            {
                targets.Add(e as Character);
            } 

            foreach (Skill skill in skills)
            {
                playerActions.Add(new PlayerAction(skill, targets));
            }

            ResolvedResult resolvedResult = Resolve.resolve(playerActions);

            skillUseSummaryUI.summaryText.text = resolvedResult.description;
            break;

            case 1: 
                skillConfirmButtonText.text = "Confirm";
                CloseCardCloseUp(true);
                skillConfirmButtonState = 0;
                Debug.Log("resolve 1 " + Control.control.players[Control.control.currentPlayerCharacterIndex].resolve);
                Control.control.players[Control.control.currentPlayerCharacterIndex].resolve = Control.control.players[Control.control.currentPlayerCharacterIndex].resolve + resolveToBeAdded;
                Debug.Log("resolve 2 " + Control.control.players[Control.control.currentPlayerCharacterIndex].resolve);
                break;

        }
        UpdateCurrentCharView();
    }


    public void CheckIfReadyToConfirmSkills()
    {
        List<Skill> skills = skillCardUIcloseUp.ReturnSelectedSkills();
        if (skills.Count > 0 && selectedEnemies.Count > 0)
        {
            Control.control.confirmCharacterSelectionButton.gameObject.SetActive(true);
        }
        else
        {
            Control.control.confirmCharacterSelectionButton.gameObject.SetActive(false);
        }
    }

}
