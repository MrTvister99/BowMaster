using UnityEngine;

public interface IPlayerInput
{
    Vector2 GetAimDirection();
    bool IsCharging { get; }
    void StartCharging();
    void StopCharging();
    Vector3 GetStartPosition();
    Vector3 GetCurrentPosition();
} 