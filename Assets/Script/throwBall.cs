using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwBall : MonoBehaviour
{
    public GameObject ball;
    public GameManagerNew GameManagerNew;

    [Header("Movement (A <-> B)")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;

    [Header("Throwing")]
    public bool throwDone;
    public float secondsPerBall = 0.5f;

    private float _t;
    private Coroutine _throwLoop;

    private void Start()
    {
        if (GameManagerNew == null)
        {
            GameManagerNew = FindFirstObjectByType<GameManagerNew>();
        }

        if (pointA != null)
        {
            transform.position = pointA.position;
        }

        _throwLoop = StartCoroutine(ThrowLoop());
    }

    private void Update()
    {
        if (pointA == null || pointB == null) return;

        float distance = Vector3.Distance(pointA.position, pointB.position);
        if (distance <= 0.0001f) return;

        _t += (moveSpeed / distance) * Time.deltaTime;
        float pingPong = Mathf.PingPong(_t, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, pingPong);
    }

    private IEnumerator ThrowLoop()
    {
        while (true)
        {
            float cooldown = GameManagerNew != null ? GameManagerNew.BallCooldownMax : 0f;
            int amount = GameManagerNew != null ? GameManagerNew.BallAmountMax : 0;

            if (cooldown > 0f)
            {
                yield return new WaitForSeconds(cooldown);
            }
            else
            {
                yield return null;
            }

            if (ball == null || amount <= 0)
            {
                continue;
            }

            throwDone = false;
            for (int i = 0; i < amount; i++)
            {
                Instantiate(ball, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(secondsPerBall);
            }
            throwDone = true;
        }
    }
}
