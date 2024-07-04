using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cover : MonoBehaviour
{
   public ParticleSystem ParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "+1")
        { 
            Destroy(collision.gameObject);
            ParticleSystem.Play();
        }
    }
}
