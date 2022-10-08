using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillCardUI : MonoBehaviour, IPointerClickHandler
{

    public TextMeshProUGUI SkillCardName;
    public TextMeshProUGUI baseSkillText;
    public List<RectTransform> skillUIs;
    public RectTransform skillsParent;
    public SkillCard skillCard;

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
        if (Control.control.uiMode == Control.UImode.PlayerTurn)
        {
            Control.control.InitializeCardCloseUp(skillCard);
        }
    }
}
