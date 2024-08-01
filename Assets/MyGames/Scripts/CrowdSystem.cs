using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParents;
    [SerializeField] private GameObject runnerPrefabs;

    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;   

    void Update()
    {
        PlaceRunner();
    }

    private void PlaceRunner()
    {
        for (int i = 0; i < runnersParents.childCount; i++)
        {
            Vector3 childLocalPosition = PlayerRunnerLocalPositions(i);
            runnersParents.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 PlayerRunnerLocalPositions(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParents.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnerToAdd = (runnersParents.childCount * bonusAmount) - runnersParents.childCount;
                AddRunners(runnerToAdd);
                break;

            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Division:
                int runnerToRemove = runnersParents.childCount - (runnersParents.childCount / bonusAmount);
                RemoveRunners(runnerToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefabs, runnersParents);
        }
    }

    private void RemoveRunners(int amount)
    {
        if(amount > runnersParents.childCount)
        {
            amount = runnersParents.childCount;
        }
        int runnersAmount = runnersParents.childCount;
        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParents.GetChild(i);
            runnerToDestroy.SetParent(null); // gỡ bỏ runnerToDestroy thứ i khỏi cha hiện tại của nó
            Destroy(runnerToDestroy.gameObject);
        }
    }


}
