using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Vector2 NewVelocity { get; private set; }
    public bool IsAvoiding { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        IsAvoiding = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.position.x > transform.position.x)
        {
            NewVelocity = new Vector2(-1, -1);
        }
        else
        {
            NewVelocity = new Vector2(1, 1);
        }
    }

    public void SetIsAvoidingFalse() => IsAvoiding = false;

}
