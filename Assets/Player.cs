using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float slideAmount = 3f;
    public Transform playerCamera;
    public float lookSpeed = 100f;
    public float maxLookAngle = 80f;
    public float climbSpeed = 3f;
    public Vector3 velocity = Vector3.zero;
    public bool touchingLadder = false;
    public float jumpHeight = 3f;
    private float jumpTimer = 0f;
    private bool canJump = true;
    private float cameraPitch = 0f;

    void Start()
    {
        //finds a camera
        if (playerCamera == null)
        {
            Camera cam = GetComponentInChildren<Camera>();

            if (cam != null)
            {
                playerCamera = cam.transform;
            }
        }
    }

    void Update()
    {
        RotateVision();
        MovePlayer();

        //timer for jumps
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f)
            {
                canJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transform.position += Vector3.up * jumpHeight;
            jumpTimer = 1f;
            canJump = false;
        }
    }

    void RotateVision()
    {
        if (playerCamera == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -lookSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, lookSpeed * Time.deltaTime);
        }

        float verticalLook = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalLook = 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalLook = -1f;
        }

        cameraPitch -= verticalLook * lookSpeed * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void MovePlayer()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && !touchingLadder)
        {
            input += transform.forward;
        }

        if (Input.GetKey(KeyCode.S) && !touchingLadder)
        {
            input -= transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input -= transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input += transform.right;
        }

        input.y = 0f;
        input = input.normalized;

        Vector3 targetVelocity = input * moveSpeed;

        velocity = Vector3.Lerp(velocity,targetVelocity, slideAmount * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;

        if (touchingLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * climbSpeed * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            touchingLadder = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            touchingLadder = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            touchingLadder = false;
        }
    }
}