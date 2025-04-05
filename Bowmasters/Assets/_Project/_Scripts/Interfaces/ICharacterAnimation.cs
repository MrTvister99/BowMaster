using UnityEngine;
using Spine;

public interface ICharacterAnimation
{
    void UpdateTilt(float angle);
    void StartAimAnimation();
    void PlayAttackAnimation();
    void ResetRotations();
    void Initialize();
    void IdleAnimation();
} 