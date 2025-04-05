using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(ShootSystem))]
[RequireComponent(typeof(TrajectoryRenderer))]
public class BowController : MonoBehaviour
{
    private IPlayerInput playerInput;
    private ICharacterAnimation characterAnimation;
    private IShootSystem shootSystem;
    private ITrajectoryRenderer trajectoryRenderer;

    private float chargeStartTime;
    private bool isCharging;
    private Vector3 startPosition;
    bool state = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterAnimation = GetComponent<CharacterAnimation>();
        shootSystem = GetComponent<ShootSystem>();
        trajectoryRenderer = GetComponent<ITrajectoryRenderer>();
        characterAnimation.Initialize();
    }

    private void Update()
    {
        HandleInput();
        if (isCharging)
        {
            UpdateAiming();
        }
        else
        {
            Debug.Log("ww");
           
           
            characterAnimation.IdleAnimation();
           
            characterAnimation.UpdateTilt(0f);
        }
    }

    private void HandleInput()
    {
        if (!isCharging && Input.GetMouseButtonDown(0))
        {
            StartCharging();
        }
        else if (isCharging && Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
        startPosition = shootSystem.GetBowPosition();

        playerInput.StartCharging();
        shootSystem.StartAiming();
        characterAnimation.StartAimAnimation();
        trajectoryRenderer.ShowTrajectory(startPosition, playerInput.GetAimDirection(), 0f);
    }

    private void UpdateAiming()
    {
        Vector2 aimDirection = playerInput.GetAimDirection();
        startPosition = shootSystem.GetBowPosition();
        float chargeTime = Time.time - chargeStartTime;

        float angle = -Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        characterAnimation.UpdateTilt(angle);

        shootSystem.UpdateAiming(aimDirection, chargeTime);
        trajectoryRenderer.UpdateTrajectory(startPosition, aimDirection,shootSystem.GetCurrentForce());
    }

    private void Shoot()
    {
        if (!isCharging) return;

        Vector2 aimDirection = playerInput.GetAimDirection();
        float chargeTime = Time.time - chargeStartTime;

        characterAnimation.PlayAttackAnimation();
        shootSystem.Shoot(aimDirection, chargeTime);
        trajectoryRenderer.HideTrajectory();

        isCharging = false;
        playerInput.StopCharging();
        ResetState();
    }

    private void ResetState()
    {
        characterAnimation.ResetRotations();
    }
} 