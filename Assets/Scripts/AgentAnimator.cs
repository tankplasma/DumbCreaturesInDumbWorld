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

    string walk;
    string jump;
    string beginJump;
    string fall;
    string swim;

    public void ChangeAnimState()
    {

    }
}
