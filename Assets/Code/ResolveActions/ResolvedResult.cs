using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolvedResult
{
    public string description;

    public ResolvedResult() {
        this.description = "";
    }

    public void addToDescription(string desc) {
        description += desc + ", ";
    }

}