using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCountertext;
    [SerializeField] private Transform runnersParent;
    
    void Update()
    {
        crowdCountertext.text = runnersParent.childCount.ToString();
    }
}
