using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairBehaviour : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveBack() {
        animator.SetTrigger("MoveChairBack");
    }

    public void MoveForward() {
        animator.SetTrigger("MoveChairForward");
    }
}
