using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class Pizza : MonoBehaviour, IInteractable
{
    public bool canInteract => pizzaLeft > 0 && Time.time >= _nextInteractAllowedTime;

    public Animator animator;
    public MMF_Player onHitFeedBack;
    public MMF_Player onGrowFeedBack;
    public int pizzaLeft;
    public CircleCollider2D Ccollider2D;

    public GameObject PizzaSlice;
    public float force;

    [Tooltip("Seconds after the last slice before pizza refills to 8.")]
    public float refillDelaySeconds = 5f;

    [Tooltip("Minimum time between two slice throws.")]
    public float interactCooldownSeconds = 0.35f;

    private float _nextInteractAllowedTime;
    private Coroutine _recoverRoutine;

    public void InteractWith()
    {
        if (pizzaLeft <= 0) return;
        if (Time.time < _nextInteractAllowedTime) return;

        onHitFeedBack.PlayFeedbacks();

        pizzaLeft -= 1;
        FirePizza();

        _nextInteractAllowedTime = Time.time + interactCooldownSeconds;

        if (pizzaLeft == 0 && _recoverRoutine == null)
        {
            _recoverRoutine = StartCoroutine(RecoverAfterDelay());
        }
    }

    void Update()
    {
        if (pizzaLeft == 0)
        {
            animator.SetInteger("PizzaLeft", 0);
            Ccollider2D.enabled = false;
        }
        else
        {
            Ccollider2D.enabled = true;
            animator.SetInteger("PizzaLeft", pizzaLeft);
        }
    }

    private IEnumerator RecoverAfterDelay()
    {
        yield return new WaitForSeconds(refillDelaySeconds);

        onGrowFeedBack.PlayFeedbacks();
        pizzaLeft = 8;
        animator.SetInteger("PizzaLeft", pizzaLeft);
        _recoverRoutine = null;
    }

    private void FirePizza()
    {
        // After decrement: 1st slice -> pizzaLeft 7 -> index 1 -> 45°; 2nd -> 90°; ... 8th -> 360°
        int sliceIndex = 8 - pizzaLeft;
        float angleDeg = 45f * sliceIndex;
        Quaternion rot = Quaternion.Euler(0f, 0f, angleDeg);
        Vector2 dir = rot * Vector2.up;

        GameObject slice = Instantiate(PizzaSlice, transform.position, rot);
        Rigidbody2D rb = slice.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
}
