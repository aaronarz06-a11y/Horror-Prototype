using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform[] checkpoints;
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    public float reachDistance = 0.2f;
    private int currentCheckpoint = 0;

    void Update()
    {
        //checks if player is close enough to chase
        if (player != null &&
            Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            FollowCheckpoints();
        }
    }

    //chase the player
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    //fly's between checkpoints
    void FollowCheckpoints()
    {
        if (checkpoints.Length == 0)
        {
            return;
        }

        Transform targetCheckpoint = checkpoints[currentCheckpoint];

        if (targetCheckpoint == null)
        {
            return;
        }

        //movement
        transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.position, moveSpeed * Time.deltaTime);

        //move to the next checkpoint when close enough
        if (Vector3.Distance(transform.position, targetCheckpoint.position) <= reachDistance)
        {
            currentCheckpoint++;

            //loops checkpoints
            if (currentCheckpoint >= checkpoints.Length)
            {
                currentCheckpoint = 0;
            }
        }
    }
}