using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private float detectionRadius = 1f;

    private void Update()
    {
        DetectDoors();
    }
    private void DetectDoors()
    {
        // OverlapSphere Tạo một hình cầu vô hình
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                Debug.Log("Hit the doors");
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisableDoorCollider();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
            else if (detectedColliders[i].tag == "Finish")
            {
                Debug.Log("Hit Finish Line");
                SceneManager.LoadScene(0);
            }

        }
    }

    // Debug kiểm tra xem vị trí của hình cầu vô hình đã tạo overlapSphere
    private void OnDrawGizmosSelected()
    {
        // Vẽ hình cầu phát hiện để kiểm tra trong Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
