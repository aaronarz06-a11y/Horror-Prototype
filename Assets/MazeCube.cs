using UnityEngine;

public class MazeCube : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Transform startPosition;
    public MazePuzzleConsole puzzleConsole;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetToStart();
    }

    void FixedUpdate()
    {
        if (puzzleConsole == null || !puzzleConsole.puzzleActive)
        {
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        //move using Y and Z axes
        Vector3 movement = new Vector3(0f, vertical, horizontal).normalized;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //start from begining
    public void ResetToStart()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (startPosition != null)
        {
            rb.position = startPosition.position;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //if wall is touched
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MazeWall"))
        {
            ResetToStart();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MazeGoal"))
        {
            puzzleConsole.CompletePuzzle();
        }
    }
}