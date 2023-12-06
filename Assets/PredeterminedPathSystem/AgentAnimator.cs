using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimType
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

    public void ChangeAnimState(AnimType type)
    {

    }
}
