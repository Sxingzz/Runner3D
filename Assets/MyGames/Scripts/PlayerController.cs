using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidth;
    [Header("Control")]
    [SerializeField] private float slidSpeed; 

    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    [SerializeField] private CrowdSystem crowdSystem;
   

    private void Start()
    {
        
    }

    private void Update()
    {
        MoveSpeedForward();
        ManageControl();


    }

    private void MoveSpeedForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickScreenPosition = Input.mousePosition; // vị trí click chuột ban đầu
            clickPlayerPosition = transform.position; // vị trí hiện tại player
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickScreenPosition.x;

            xScreenDifference /= Screen.width; // chuẩn hóa theo chiều rộng screen
            xScreenDifference *= slidSpeed; 

            Vector3 position = transform.position; // vị trí hiện tại player
            position.x = clickPlayerPosition.x + xScreenDifference; // set vị trí mới player

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), 
                                      roadWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position; // cập nhật vị trí mới player

            //transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
