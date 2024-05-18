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


    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;

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

        gameOverScoreUI.text = "Score: " + gameManager.PrettyScore();
        gameOverHighscoreUI.text = "Highscore: " + gameManager.PrettyHighscore();
    }
    private void OnGUI()
    {
        scoreGUI.text = gameManager.PrettyScore();
    }
}
