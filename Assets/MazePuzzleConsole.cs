using UnityEngine;

public class MazePuzzleConsole : MonoBehaviour
{
    public Player playerController;
    public Transform playerCamera;
    public Transform mazeCameraPosition;
    public Transform cubeStartPosition;
    public MazeCube mazeCube;
    public Light flashlight;
    public Light mazeLight;
    public float interactionDistance = 3f;
    public bool puzzleActive = false;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private bool originalFlashlightState;

    void Start()
    {
        //light for maze is off
        if (mazeLight != null)
        {
            mazeLight.enabled = false;
        }

        if (mazeCube != null)
        {
            mazeCube.puzzleConsole = this;

            if (cubeStartPosition != null)
            {
                mazeCube.startPosition = cubeStartPosition;
            }
        }
    }

    void Update()
    {
        if (playerController == null || playerCamera == null)
        {
            return;
        }

        //press E near the console to start the maze
        if (!puzzleActive && Vector3.Distance(playerController.transform.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            StartPuzzle();
        }
    }

    void StartPuzzle()
    {
        if (mazeCameraPosition == null || mazeCube == null)
        {
            return;
        }

        //saves the players original camera view
        originalCameraPosition = playerCamera.position;
        originalCameraRotation = playerCamera.rotation;

        //remembers if the flashlight was on
        if (flashlight != null)
        {
            originalFlashlightState = flashlight.enabled;
            flashlight.enabled = false;
        }

        //switches camera placement
        playerCamera.SetPositionAndRotation(mazeCameraPosition.position, mazeCameraPosition.rotation);

        //disable player movement and camera controls
        playerController.enabled = false;

        //turns on the maze light
        if (mazeLight != null)
        {
            mazeLight.enabled = true;
        }

        mazeCube.ResetToStart();
        puzzleActive = true;
    }

    public void CompletePuzzle()
    {
        if (!puzzleActive)
        {
            return;
        }

        //restores the players original camera view, movement, andcamera controls
        playerCamera.SetPositionAndRotation(originalCameraPosition, originalCameraRotation);
        playerController.enabled = true;

        //restores the flashlight to its previous state
        if (flashlight != null)
        {
            flashlight.enabled = originalFlashlightState;
        }

        //turn off the maze light
        if (mazeLight != null)
        {
            mazeLight.enabled = false;
        }

        puzzleActive = false;
    }
}