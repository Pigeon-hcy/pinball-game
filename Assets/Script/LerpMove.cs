using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour
{

    public Vector3 endPosition;
    public Vector3 startPosition;
    public float desiredDuration = 3.0f;
    private float elapsedTime;

    [SerializeField]private AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));
    }
}
