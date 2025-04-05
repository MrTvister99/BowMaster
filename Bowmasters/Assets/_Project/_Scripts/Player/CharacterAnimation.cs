using UnityEngine;
using Spine.Unity;

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    [SerializeField] private float maxTiltAngle = 45f;
    [SerializeField] private float tiltSmoothness = 8f;

    private bool stateIdle=false;
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private float currentTiltAngle = 0f;
    private float targetTiltAngle = 0f;

    private Spine.Bone rootBone;
    private Spine.Bone charBone;
    private Spine.Bone bodyBone;
    private Spine.Bone plevisBone;
    private Spine.Bone legRBone;
    private Spine.Bone legLBone;
    private Spine.Bone legR2Bone;
    private Spine.Bone legL2Bone;

    public void Initialize()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        
        rootBone = skeletonAnimation.skeleton.FindBone("root");
        charBone = skeletonAnimation.skeleton.FindBone("char");
        bodyBone = skeletonAnimation.skeleton.FindBone("body");
        plevisBone = skeletonAnimation.skeleton.FindBone("plevis");
        legRBone = skeletonAnimation.skeleton.FindBone("legR");
        legLBone = skeletonAnimation.skeleton.FindBone("legL");
        legR2Bone = skeletonAnimation.skeleton.FindBone("legR2");
        legL2Bone = skeletonAnimation.skeleton.FindBone("legL2");

        if (rootBone == null || charBone == null || bodyBone == null || plevisBone == null || 
            legRBone == null || legLBone == null || legR2Bone == null || legL2Bone == null)
        {
            Debug.LogError("One or more required bones not found in skeleton!");
        }
    }

    public void UpdateTilt(float angle)
    {
        if (!ArebonesValid()) return;

        targetTiltAngle = -angle * 0.5f;
        targetTiltAngle = Mathf.Clamp(targetTiltAngle, -maxTiltAngle, maxTiltAngle);

        currentTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, Time.deltaTime * tiltSmoothness);

        float rootTilt = currentTiltAngle * 0.2f; 
        float charTilt = currentTiltAngle * 0.2f;  
        float bodyTilt = currentTiltAngle * 0.4f;  
        float legTilt = currentTiltAngle * 0.3f;   

        rootBone.Rotation = rootTilt;
        charBone.Rotation = charTilt;
        bodyBone.Rotation = 90f + bodyTilt;

        plevisBone.Rotation = currentTiltAngle * 0.1f;

        legRBone.Rotation = -78.77f - legTilt;
        legR2Bone.Rotation = -27.65f + legTilt * 0.5f;
        legLBone.Rotation = -81.5f - legTilt;
        legL2Bone.Rotation = -17.4f + legTilt * 0.5f;

        skeletonAnimation.skeleton.UpdateWorldTransform();
    }

    public void StartAimAnimation()
    {
        animationState.SetAnimation(0, "attack_start", false);
    }

    public void PlayAttackAnimation()
    {
        animationState.SetAnimation(0, "attack_finish", false);
        stateIdle = false;
    }

    public void IdleAnimation()
    {
        if (stateIdle == false)
        {
            stateIdle = true;
            animationState.SetAnimation(0, "idle", true);
        }
    }

    public void ResetRotations()
    {
        if (!ArebonesValid()) return;

        targetTiltAngle = 0f;
        currentTiltAngle = 0f;

        rootBone.Rotation = 0f;
        charBone.Rotation = 0f;
        bodyBone.Rotation = 90f;
        plevisBone.Rotation = 0f;
        legRBone.Rotation = -78.77f;
        legLBone.Rotation = -81.5f;
        legR2Bone.Rotation = -27.65f;
        legL2Bone.Rotation = -17.4f;
        
        skeletonAnimation.skeleton.UpdateWorldTransform();
    }

    private bool ArebonesValid()
    {
        return rootBone != null && charBone != null && bodyBone != null && plevisBone != null && 
               legRBone != null && legLBone != null && legR2Bone != null && legL2Bone != null;
    }
} 