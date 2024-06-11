using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nearRule : MonoBehaviour
{
    public GameManger manger;
    
    public SpriteRenderer spriteRenderer;
    private void Awake()
    {
        manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.enabled = false;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "near")
        {
            manger.noNearBlock = true;
            spriteRenderer.enabled = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "near")
        {
            manger.noNearBlock = true;
            spriteRenderer.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "near")
        {
            manger.noNearBlock = false;
            spriteRenderer.enabled = false;
        }
    }
}
