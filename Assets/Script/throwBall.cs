using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwBall : MonoBehaviour
{
    public GameObject ball;
    public GameObject errorMessage;
    public Rigidbody2D rb;
    public GameManger GameManger;
    public PhaseManger PhaseManger;
    public bool throwDone;

    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;

    //public delegate void TurnEndAction();
    //public static TurnEndAction OnTurnEnd;


    private void Awake()
    {
        PhaseManger = GameObject.FindGameObjectWithTag("PhaseManger").GetComponent<PhaseManger>();
    }

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            { 
                i =0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    public void startThrowBall()
    {
        if (PhaseManger.turnStart == true && GameManger.noNearBlock == false)
        {
            StartCoroutine(throwNumberBall(GameManger.Emotion));
        }else if (GameManger.noNearBlock == true) 
        {
            errorMessage.SetActive(true);
            errorMessage.gameObject.GetComponent<lifeTime>().LifeTime = 1000;
        }
         
    }

    IEnumerator throwNumberBall(int Emotion)
    {
        
        throwDone = false;
        PhaseManger.changePhase(2);
        GameManger.tryTime--;
        for (int i = 0; i < Emotion; i++)
        {
            Instantiate(ball, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);

        }
        //if (OnTurnEnd != null)
        //{
        //    OnTurnEnd();
        //}

        throwDone = true;
        
    }
}
