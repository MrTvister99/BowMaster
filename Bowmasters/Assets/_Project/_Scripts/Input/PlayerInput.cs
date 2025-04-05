using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Camera mainCamera;
    private Vector3 startMousePosition;
    private bool isCharging;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartCharging()
    {
        startMousePosition = Input.mousePosition;
        isCharging = true;
    }

    public void StopCharging()
    {
        isCharging = false;
    }

    public Vector2 GetAimDirection()
    {
        if (!isCharging) return Vector2.zero;

        Vector3 worldStartPosition = mainCamera.ScreenToWorldPoint(new Vector3(startMousePosition.x, startMousePosition.y, mainCamera.nearClipPlane));
        Vector3 worldCurrentPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        
        return (worldStartPosition - worldCurrentPosition).normalized;
    }

    public bool IsCharging => isCharging;

    public Vector3 GetStartPosition()
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(startMousePosition.x, startMousePosition.y, mainCamera.nearClipPlane));
    }

    public Vector3 GetCurrentPosition()
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
    }
} 