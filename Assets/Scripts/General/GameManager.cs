using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public float currentScore = 0f;
    public SaveData saveData;
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent(); 
    public UnityEvent onGameOver = new UnityEvent();


    private void Start()
    {
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            saveData = JsonUtility.FromJson<SaveData>(loadedData);
        } else
        {
            saveData = new SaveData();
        }
        
    }

    private void Update()
    {
        if(isPlaying)
        {
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
    }

    public void GameOver()
    {
        if (saveData.highscore < currentScore)
        {
            saveData.highscore = currentScore;
            string saveString = JsonUtility.ToJson(saveData);
            SaveSystem.Save("save", saveString);
        }
        isPlaying = false;
        onGameOver.Invoke();
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(saveData.highscore).ToString();
    }
}
