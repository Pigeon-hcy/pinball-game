using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndLogic : MonoBehaviour
{
    public void turnEnd()
    {
        gameObject.GetComponent<ITurnEnd>().TurnEndEffect();
    }
}

