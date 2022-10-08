using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control control;
    public float damage;

    public List<float> slot1probabilities;
    public List<float> slot2probabilities;
    public List<float> slot3probabilities;
    public List<float> slot4probabilities;
    public List<float> slot5probabilities;

    // Start is called before the first frame update
    void Awake()
    {
        control = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }


}
