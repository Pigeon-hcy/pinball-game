using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSliceBehave : MonoBehaviour
{
    public Transform Pizza;
    public Rigidbody2D Rigidbody;
    private float zRoation;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Rigidbody.velocity.y > 0)
        {
            if (zRoation != 0)
                zRoation -= 1;
        }
    }
}
