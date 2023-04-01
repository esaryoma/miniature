using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUseReactionUI : MonoBehaviour
{
    public TextMeshProUGUI reactionField;

    public void DisplayReaction(string reactionText)
    {
        gameObject.SetActive(true);
        reactionField.text = reactionText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
