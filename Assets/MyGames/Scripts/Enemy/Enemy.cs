using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State { Idle, Running}

    [Header("Setting")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;

    private State state;
    private Transform targetRunner;

    private void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;

            case State.Running:
                RunnerTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Running");
    }

    private void RunnerTowardsTarget()
    {
        if (targetRunner == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject); // player
            Destroy(gameObject); // enemy
        }
    }

}
