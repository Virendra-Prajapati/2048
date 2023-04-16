using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Canvas uiCanvas;
    public RectTransform gameOverPanel;

    private void Awake()
    {
        if(instance == null) instance = this;
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
        gameOverPanel.SetParent(uiCanvas.transform);
    }

    public void RestartButtonClicked()
    {
        CloseGameOverPanel();
        BoardController.instance.RestartGame();
    }

}
