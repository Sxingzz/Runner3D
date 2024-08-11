using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidth;
    [SerializeField] private bool canMove;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Control")]
    [SerializeField] private float slidSpeed; 

    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    [SerializeField] private CrowdSystem crowdSystem;
   
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallBack;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveSpeedForward();
            ManageControl();
        }
    }

    private void GameStateChangeCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if (gameState == GameManager.GameState.GameOver)
        {
            StopMoving();
        }
    }

    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
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
