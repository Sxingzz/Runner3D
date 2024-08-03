using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text LevelText;

    private void Start()
    {
        progressBar.value = 0;

        LevelText.text = "Level " + (ChunkManager.instance.GetLevels() + 1).ToString();
    }
    private void Update()

    {
        UpdateProgressBar();
    }
    public void Play_Button_Pressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState()) return;
        float progress = PlayerController.instance.transform.position.z 
                            / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;

    }

    
}
