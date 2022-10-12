using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class AnimationsManager : MonoBehaviour
{
    Animator animator;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
    }
}
