using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    public TextMeshProUGUI timerText;
    public float _elapsedTime = 0f;
    private bool _isRunning = true;

    private int _minutes;
    private int _seconds;
    private int _milliSeconds;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Listen for scene changes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if(!_isRunning) return;

        _elapsedTime += Time.deltaTime;
        UpdateTimerUi();
    }

    private void UpdateTimerUi()
    {
        _minutes= Mathf.FloorToInt(_elapsedTime/60);
        _seconds= Mathf.FloorToInt(_elapsedTime%60);
        _milliSeconds= Mathf.FloorToInt((_elapsedTime*100)%100);

        timerText.text=string.Format("{0:00}:{1:00}:{2:00}",_minutes, _seconds, _milliSeconds);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the timer text in the new scene
        timerText = GameObject.FindWithTag("TimerText")?.GetComponent<TextMeshProUGUI>();
    }
    public void StopTimer()
    {
        _isRunning = false;
    }

    public float ReturnTotalTime()
    {
        return _elapsedTime;
    }
}
