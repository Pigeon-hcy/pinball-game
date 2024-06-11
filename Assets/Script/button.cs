using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public PhaseManger PhaseManger;
    public Image image;

    private void Awake()
    {
        PhaseManger = GameObject.FindGameObjectWithTag("PhaseManger").GetComponent<PhaseManger>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PhaseManger.turnStart)
        {
            image.color = Color.white;
        }
        else if (PhaseManger.turnEnd) 
        {
            image.color = Color.grey;
        }
    }
}
