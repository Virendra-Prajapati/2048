using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Canvas uiCanvas;
    public RectTransform gameOverPanel;
    public RectTransform startGamePanel;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    private void Start()
    {
        OpenStartGamePanel();
    }

    public void OpenStartGamePanel()
    {
        startGamePanel.SetParent(uiCanvas.transform);
        startGamePanel.sizeDelta = Vector2.zero;
        startGamePanel.localPosition = Vector2.zero;
        startGamePanel.gameObject.SetActive(true);
    }

    public void CloseStartGamePanel()
    {
        startGamePanel.gameObject.SetActive(false);
        startGamePanel.SetParent(transform);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetParent(uiCanvas.transform);
        gameOverPanel.sizeDelta = Vector2.zero;
        gameOverPanel.localPosition = Vector3.zero;
        gameOverPanel.gameObject.SetActive(true);
    }

    private void CloseGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(false);
        gameOverPanel.SetParent(transform);
    }

    public void RestartButtonClicked()
    {
        CloseGameOverPanel();
        BoardController.instance.RestartGame();
    }

    public void StartButtonClicked()
    {
        CloseStartGamePanel();
        BoardController.instance.StartGame();
    }

}
