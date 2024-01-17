using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Tilemaps;

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


    Vector3 pos;
    Vector3 lastPos;
    Vector3 dir;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pos, 0.01f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastPos, 0.01f);

        Debug.DrawRay(pos, dir * 0.01f , Color.green);

    }

    public void ChangeAnimState(EAnimType type)
    {
        switch (type)
        {
            case EAnimType.Walk:
                animator.SetTrigger(walk);
                break;
            case EAnimType.Jump:
                animator.SetTrigger(jump);
                break;
            case EAnimType.BeginJump:
                animator.SetTrigger(beginJump);
                break;
            case EAnimType.Fall:
                animator.SetTrigger(fall);
                break;
            case EAnimType.Swim:
                animator.SetTrigger(swim);
                break;
            default:
                break;
        }
    }
    
    public void SetIKWeight(float weight) 
    { 
        IKRig.weight = weight;
    }

    public void SetMemberPositionTo(AvatarIKGoal type , Vector3 pos , float speed)
    {
        Transform stockT = null;

        switch (type)
        {
            case AvatarIKGoal.LeftFoot:
                stockT = leftFootPoint;
                leftFootPoint.position = pos;
                break;
            case AvatarIKGoal.RightFoot:
                stockT= rightFootPoint;
                rightFootPoint.position = pos;
                break;
            case AvatarIKGoal.LeftHand:
                stockT = leftHandPoint;
                leftHandPoint.position = pos;
                break;
            case AvatarIKGoal.RightHand:
                stockT = rightHandPoint;
                rightHandPoint.position = pos;
                break;
        }
        //StartCoroutine(MoveToPos(stockT, pos, speed));
    }

    IEnumerator MoveToPos(Transform posToMove , Vector3 posToReach , float speed)
    {
        Vector3 Direction = (posToReach - posToMove.position).normalized;
        Debug.Log(Direction.magnitude);
        dir = Direction;
        pos = posToMove.position;
        lastPos = posToReach;

        Vector3 initialPos = posToMove.position;

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(initialPos , posToReach, time/0.5f);

            posToMove.position = newPos;

            yield return new WaitForEndOfFrame();
        }
    }
}
