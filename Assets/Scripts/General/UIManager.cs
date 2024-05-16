using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreGUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    GameManager gameManager;

    private void Start()
    {
        scoreGUI.gameObject.SetActive(false);
        gameManager = GameManager.instance;
        gameManager.onGameOver.AddListener(ActivateGameOverUI);
    }

    public void PlayButtonHandler()
    {
        gameManager.StartGame();
        scoreGUI.gameObject.SetActive(true);
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
    private void OnGUI()
    {
        scoreGUI.text = gameManager.PrettyScore();
    }
}
