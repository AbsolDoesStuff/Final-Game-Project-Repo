using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float xThreshold = 8f; // X position where camera switches (X = 8)
    [SerializeField] private float cameraPositionLeft = 0f; // Camera X position when player is left of threshold (X = 0)
    [SerializeField] private float cameraPositionRight = 15f; // Camera X position when player is right of threshold (X = 15)
    [SerializeField] private float transitionDuration = 0.5f; // Duration of the smooth transition in seconds

    private bool isPlayerOnRight; // Tracks if player is on the right side of the threshold
    private bool isTransitioning; // Prevents multiple transitions from overlapping

    void Start()
    {
        // Find the player if not assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found! Please assign the player in the Inspector.");
                return;
            }
        }

        // Initialize based on player's starting position
        isPlayerOnRight = player.position.x > xThreshold;
        UpdateCameraPosition();
    }

    void Update()
    {
        if (player == null) return;

        // Check if player crosses the threshold
        bool playerIsOnRightNow = player.position.x > xThreshold;

        // If the player's side changes and we're not already transitioning, move the camera
        if (isPlayerOnRight != playerIsOnRightNow && !isTransitioning)
        {
            isPlayerOnRight = playerIsOnRightNow;
            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition()
    {
        // Calculate the target position, keeping Y and Z unchanged
        Vector3 targetPosition = transform.position;
        targetPosition.x = isPlayerOnRight ? cameraPositionRight : cameraPositionLeft;

        // Start the smooth transition
        StartCoroutine(SmoothMoveCamera(targetPosition, transitionDuration));
    }

    private System.Collections.IEnumerator SmoothMoveCamera(Vector3 targetPosition, float duration)
    {
        isTransitioning = true; // Prevent overlapping transitions
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            // Lerp the position over time
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera reaches the exact target position
        transform.position = targetPosition;
        isTransitioning = false; // Transition complete
    }
}