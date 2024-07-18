using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParents;


    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;   

    void Start()
    {
        
    }

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
}
