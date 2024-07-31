using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall_interactLogic : MonoBehaviour
{
    public GameManger GameManger;
    public GameObject MParticle;
    private void Awake()
    {
        GameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().InteractWith();
            
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            collision.gameObject.GetComponent<IInteractable>().InteractWith();
            StartCoroutine(AddEmo());
        }
    }

    IEnumerator AddEmo()
    {
        
        for (int i = 0; i < 4; i++)
        {
            Instantiate(MParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            GameManger.Score += 1;
        }
        yield return new WaitForSeconds(1);
        
    }
}
