using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
    //Find the player
    //Find distance between player and self
    //Get a distance behind the player and move toward that
    //Follows a small curve, waits and then repeats
    //After 10 seconds kill
    [SerializeField] float moveSpeed;

    private float timeSinceLastAttack;
    bool isMovingTowards;
    bool playerFound;
    bool lookingForPlayer;
    bool timer;
    Player player;
    Vector2 targetLocation;
    [SerializeField] private int trackCD;

    void Start()
    {
        player = FindObjectOfType<Player>();
        timeSinceLastAttack = Time.time;
        isMovingTowards = false;
        playerFound = false;
        lookingForPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        timer = Time.time >= timeSinceLastAttack + trackCD;
        if (playerFound && !lookingForPlayer)
        {
            Move();
        }
    }

    private void UpdateTime()
    {
        timeSinceLastAttack = Time.time;
    }

    private void Move() //WORKS! But, doubles up on search idk why and need to figure how to curve it
    {
        if (!lookingForPlayer)
        {
            isMovingTowards = true;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, targetLocation, movementThisFrame);
            if (new Vector2(transform.position.x, transform.position.y) == targetLocation)
            {
                UpdateTime();
                isMovingTowards = false;
                playerFound = false;
            }
        }
    }

    private void FindTarget()
    {
        if (timer & !playerFound)
        {
            lookingForPlayer = true;
            isMovingTowards = false;
            var targetX = player.transform.position.x;
            var targetY = player.transform.position.y;


            if (player.transform.position.x > gameObject.transform.position.x)
            {
                targetX++;
            }
            else
            {
                targetX--;
            }

            if (player.transform.position.y > gameObject.transform.position.y)
            {
                targetY++;
            }
            else
            {
                targetY--;
            }

            targetLocation = new Vector2(targetX, targetY);
            lookingForPlayer = false;
            playerFound = true;
        }
    }
}
