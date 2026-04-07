using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallPit : MonoBehaviour
{
    public float multiplier;

    public GameManagerNew GameManagerNew;

    void Start()
    {
        GameManagerNew = FindFirstObjectByType<GameManagerNew>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManagerNew.money += (int)Mathf.Round((multiplier + GameManagerNew.pitMultiplier) * GameManagerNew.BallValue);
            Destroy(other.gameObject);
        }
    }
}
