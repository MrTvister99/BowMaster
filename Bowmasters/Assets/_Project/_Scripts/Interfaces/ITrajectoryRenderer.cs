using UnityEngine;

public interface ITrajectoryRenderer
{
    void ShowTrajectory(Vector3 origin, Vector2 direction, float force);
    void UpdateTrajectory(Vector3 origin, Vector2 directionm,float force);
    void HideTrajectory();
} 