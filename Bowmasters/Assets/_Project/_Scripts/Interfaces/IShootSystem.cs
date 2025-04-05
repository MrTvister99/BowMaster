using UnityEngine;

public interface IShootSystem
{
    void StartAiming();
    void UpdateAiming(Vector2 direction, float chargeTime);
    void Shoot(Vector2 direction, float chargeTime);
    Vector2 GetShootDirection();
    float GetCurrentForce();
    Vector3 GetBowPosition();
} 