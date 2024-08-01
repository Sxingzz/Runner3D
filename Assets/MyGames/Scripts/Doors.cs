using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType { Addition, Difference, Product, Division }
public class Doors : MonoBehaviour
{
    [Header("Elements")]
    //[SerializeField] private SpriteRenderer rightDoorRenderer;
    //[SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private MeshRenderer rightDoorRenderer;
    [SerializeField] private MeshRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private Collider collider;

    [Header("Setting")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;

    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;

    [SerializeField] private Color bonusColor;
    [SerializeField] private Color PenaltyColor;

    private void Start()
    {
        ConfigureDoors();
    }
    private void ConfigureDoors()
    {
        // Right Door Configuration
        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.material.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;

            case BonusType.Difference:
                rightDoorRenderer.material.color = PenaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;

            case BonusType.Product:
                rightDoorRenderer.material.color = bonusColor;
                rightDoorText.text = "x" + rightDoorBonusAmount;
                break;

            case BonusType.Division:
                rightDoorRenderer.material.color = PenaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;
        }

        // Left Door Configuration
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.material.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;

            case BonusType.Difference:
                leftDoorRenderer.material.color = PenaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;

            case BonusType.Product:
                leftDoorRenderer.material.color = bonusColor;
                leftDoorText.text = "#" + leftDoorBonusAmount;
                break;

            case BonusType.Division:
                leftDoorRenderer.material.color = PenaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;
        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusAmount;
        else 
            return leftDoorBonusAmount; 
    }

    public BonusType GetBonusType(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusType;
        else
            return leftDoorBonusType;
    }

    public void DisableDoorCollider()
    {
        collider.enabled = false;
    }
}
