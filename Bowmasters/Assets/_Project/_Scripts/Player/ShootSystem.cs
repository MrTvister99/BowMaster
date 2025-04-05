using UnityEngine;
using Spine.Unity;

public class ShootSystem : MonoBehaviour, IShootSystem
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float minForce = 10f;
    [SerializeField] private float maxForce = 30f;
    [SerializeField] private float maxChargeTime = 1f;
    public static Vector2 arrowPosition ;

    private bool isAiming;
    private SkeletonAnimation skeletonAnimation;
    private  Spine.Bone bowBone;
    private Vector3 startMousePosition;
    private Camera mainCamera;
    private Vector3 worldStartPosition;
    private Vector3 worldEndPosition;
    private Vector2 direction;
    private float currentForce;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        mainCamera = Camera.main;
    }

    public bool IsAiming => isAiming;

    public void StartAiming()
    {
        isAiming = true;

        bowBone = skeletonAnimation.skeleton.FindBone("bow");
        arrowPosition = bowBone.GetWorldPosition(transform);
        startMousePosition = Input.mousePosition;
        worldStartPosition = mainCamera.ScreenToWorldPoint(new Vector3(startMousePosition.x, startMousePosition.y, mainCamera.nearClipPlane));
        direction = Vector2.zero;
        currentForce = 0f;
    }

    public void UpdateAiming(Vector2 aimDirection, float chargeTime)
    {
        if (!isAiming || bowBone == null) return;

        worldEndPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        direction = (worldStartPosition - worldEndPosition).normalized;
        
        float distance = Vector3.Distance(worldStartPosition, worldEndPosition);
        float normalizedDistance = Mathf.Clamp01(distance / maxChargeTime);
        currentForce = Mathf.Lerp(minForce, maxForce, normalizedDistance);
    }

    public void Shoot(Vector2 direction, float chargeTime)
    {
        if (!isAiming || bowBone == null || direction.x <= 0) return;

        arrowPosition = bowBone.GetWorldPosition(transform);
        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        
        if (arrowScript != null)
        {
            arrowScript.Shoot(this.direction, currentForce);
        }


        isAiming = false;
    }

    public void CancelShot()
    {
        isAiming = false;
    }

    public Vector3 GetBowPosition()
    {
        bowBone = skeletonAnimation.skeleton.FindBone("bow");
        return bowBone != null ? bowBone.GetWorldPosition(transform) : transform.position;
    }

    public Vector2 GetShootDirection()
    {
        return direction;
    }

    public float GetCurrentForce()
    {
        return currentForce;
    }
} 
