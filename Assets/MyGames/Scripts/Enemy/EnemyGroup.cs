using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header("Setting")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    private void Start()
    {
        EnemyGenerate();
    }

    private void EnemyGenerate()
    {
        for (int i = 0; i < amount; i++) 
        {
            Vector3 enemyLocalPosition = EnemyRunnerLocalPositions(i);

            // TransformPoint: Chuyển đổi local space sang vị trí trong world space
            Vector3 enemyWorldPosition = enemyParent.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParent);
        }
    }

    // thuật toán tính toán phân bổ vị trí các object trong hình tròn theo hình xoắn ốc
    private Vector3 EnemyRunnerLocalPositions(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }
}