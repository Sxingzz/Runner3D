using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnerParent;

    public void Run()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Running");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Idle");
        }
    }

}
