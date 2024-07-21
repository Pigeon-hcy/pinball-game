using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTime : MonoBehaviour
{
    public int LifeTime;
    private int Lifecount = 100;
    private void Awake()
    {
        Lifecount = LifeTime;
    }
    private void Update()
    {
        Lifecount--;

        if (Lifecount < 0)
        {
            Lifecount = LifeTime;
            this.gameObject.SetActive(false);
        }
    }
}
