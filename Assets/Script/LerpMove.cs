using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour
{

    public Vector3 endPosition;
    public Vector3 startPosition;
    public float desiredDuration = 3.0f;
    private float elapsedTime;
    public int lifeTime = 300;
    public bool isE;
    public ParticleSystem EP;
    public bool isM;
    public ParticleSystem MP;

    [SerializeField]private AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        EP = GameObject.FindGameObjectWithTag("EP").GetComponent<ParticleSystem>();
        MP = GameObject.FindGameObjectWithTag("MP").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        lifeTime--;
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));

        if (lifeTime < 0)
        { 
            if (isM)
            {
                MP.Play();
            }
            if (isE)
            {
                EP.Play();
            }
            Destroy(this.gameObject);
        }
    }
}
