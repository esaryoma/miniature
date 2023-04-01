using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGdimmer : MonoBehaviour, IPointerClickHandler
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
        switch(Control.control.uiMode)
        {
            case Control.UImode.PlayerSkillCardCloseUp:
                Control.control.CloseCardCloseUp();
                break;

            case Control.UImode.UnitCardCloseUp:
                Control.control.CloseUnitCardCloseUp();
                break;
        }
         
    }
}
