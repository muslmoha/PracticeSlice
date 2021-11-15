using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D RB;
    private Vector2 CurrentVelocity;
    private Vector2 workSpace;
    private bool arrived;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SetVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAvoiding();
    }

    private void SetVelocity()
    {
        RB.velocity = new Vector2(0, movementSpeed);
        CurrentVelocity = RB.velocity;
    }

    private void SetVelocity(Vector2 newVelocity)
    {
        RB.AddForce(newVelocity);
        CurrentVelocity = RB.velocity;
    }

    //TODO get bool for avoiding and velocity for avoiding
    public void CheckIfAvoiding()
    {
        var avoiding = gameObject.GetComponentInChildren<Detection>().IsAvoiding;
        if (avoiding)
        {
            Debug.Log("I'm avoiding");
            CurrentVelocity = gameObject.GetComponentInChildren<Detection>().NewVelocity;
            SetVelocity(CurrentVelocity);
        }
    }
}
