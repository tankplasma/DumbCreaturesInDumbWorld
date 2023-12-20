using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum EAnimType
{
    Walk,
    Jump,
    BeginJump,
    Fall,
    Swim
}

public class AgentAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rig IKRig;

    [SerializeField]
    Transform rightHandPoint , leftHandPoint , rightFootPoint, leftFootPoint;

    [SerializeField]
    string walk;
    [SerializeField]
    string jump;
    [SerializeField]
    string beginJump;
    [SerializeField]
    string fall;
    [SerializeField]
    string swim;

    public void ChangeAnimState(EAnimType type)
    {

    }
    
    public void SetIKWeight(float weight) 
    { 
        IKRig.weight = weight;
    }

    public void SetMemberPositionTo(AvatarIKGoal type , Vector3 pos)
    {
        switch (type)
        {
            case AvatarIKGoal.LeftFoot:
                leftFootPoint.position = pos;
                break;
            case AvatarIKGoal.RightFoot:
                rightFootPoint.position = pos;
                break;
            case AvatarIKGoal.LeftHand:
                leftHandPoint.position = pos;
                break;
            case AvatarIKGoal.RightHand:
                rightHandPoint.position = pos;
                break;
        }
    }
}
