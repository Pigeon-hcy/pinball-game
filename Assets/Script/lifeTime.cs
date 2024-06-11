using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTime : MonoBehaviour
{
    public int LifeTime;

    private void Update()
    {
        LifeTime--;

        if (LifeTime < 0)
        { 
            this.gameObject.SetActive(false);
        }
    }
}
