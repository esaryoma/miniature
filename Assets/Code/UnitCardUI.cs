using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitCardUI : MonoBehaviour, IPointerClickHandler
{

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
        List<Skill> skills = Control.control.skillCardUIcloseUp.ReturnSelectedSkills();
        Debug.Log(skills.Count);
    }


}
