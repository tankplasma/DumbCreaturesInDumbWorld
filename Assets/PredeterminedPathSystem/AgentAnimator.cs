using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
    }

    public void SetMemberPositionTo(AvatarIKGoal type , Vector3 pos)
    {
        animator.SetIKPosition(type, pos);
    }
}
