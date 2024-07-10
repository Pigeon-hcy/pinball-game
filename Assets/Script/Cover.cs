using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cover : MonoBehaviour
{
   public ParticleSystem ParticleSystem;
    public bool Monly;
    public bool Eonly;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "M+1" && Monly)
        { 
            Destroy(collision.gameObject);
            ParticleSystem.Play();
        }

        if (collision.gameObject.tag == "S+1" && Eonly)
        {
            Destroy(collision.gameObject);
            ParticleSystem.Play();
        }
    }
}
