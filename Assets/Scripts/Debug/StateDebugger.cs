using UnityEngine;
using UnityEngine.UI;

public class StateDebugger : MonoBehaviour
{
    [Header("Debug UI")]
    public Text stateText;
    public Text velocityText;
    public Text groundedText;
    
    private PlayerController playerController;
    private Rigidbody rb;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        if (playerController != null)
        {
            rb = playerController.GetComponent<Rigidbody>();
        }
        
        // If no UI Text components assigned, create debug display
        if (stateText == null)
        {
            CreateDebugCanvas();
        }
    }

    private void Update()
    {
        if (playerController == null) return;

        // Update state display
        string currentState = "Unknown";
        if (playerController.StateMachine.IsCurrentState<IdleState>()) currentState = "Idle";
        else if (playerController.StateMachine.IsCurrentState<WalkingState>()) currentState = "Walking";
        else if (playerController.StateMachine.IsCurrentState<JumpingState>()) currentState = "Jumping";
        else if (playerController.StateMachine.IsCurrentState<FallingState>()) currentState = "Falling";
        else if (playerController.StateMachine.IsCurrentState<LandingState>()) currentState = "Landing";

        if (stateText != null)
        {
            stateText.text = $"State: {currentState}";
        }

        if (velocityText != null && rb != null)
        {
            velocityText.text = $"Velocity: {rb.velocity:F2}";
        }

        if (groundedText != null)
        {
            groundedText.text = $"Grounded: {playerController.IsGrounded}";
        }

        // Console debug
        Debug.Log($"Current State: {currentState} | Grounded: {playerController.IsGrounded} | Velocity: {rb.velocity}");
    }

    private void CreateDebugCanvas()
    {
        // Create a simple debug canvas if none exists
        GameObject canvas = new GameObject("Debug Canvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // Create state text
        GameObject stateTextObj = new GameObject("State Text");
        stateTextObj.transform.SetParent(canvas.transform);
        stateText = stateTextObj.AddComponent<Text>();
        stateText.text = "State: Unknown";
        stateText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        stateText.fontSize = 24;
        stateText.color = Color.white;
        
        RectTransform stateRect = stateTextObj.GetComponent<RectTransform>();
        stateRect.anchorMin = new Vector2(0, 1);
        stateRect.anchorMax = new Vector2(0, 1);
        stateRect.pivot = new Vector2(0, 1);
        stateRect.anchoredPosition = new Vector2(10, -10);
        stateRect.sizeDelta = new Vector2(300, 30);
    }
}