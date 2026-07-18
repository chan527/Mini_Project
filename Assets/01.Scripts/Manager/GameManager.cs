using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int survivalTime;
    private int timeUpdateInterval;

    private bool isPlaying;

    private WaitUntil waitUntilPlaying;
    private WaitForSeconds updateTimeWait;

    private Coroutine timeUpdateRoutine;

    public int SurvivalTime => survivalTime;

    public event Action<int> SurvivalTimeUpdated;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        survivalTime = 0;
        timeUpdateInterval = 1;

        isPlaying = false;

        waitUntilPlaying = new WaitUntil(() => isPlaying);
        updateTimeWait = new WaitForSeconds(timeUpdateInterval);

        GameStart();
    }

    public void GameStart()
    {
        survivalTime = 0;

        isPlaying = true;

        timeUpdateRoutine = StartCoroutine(UpdateSurvivalTimeCoroutine());
    }

    public void GamePause()
    {
        isPlaying = false;
    }

    public void GameResume()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        isPlaying = false;
        StopCoroutine(timeUpdateRoutine);
        timeUpdateRoutine = null;

        Time.timeScale = 0f;

        UIManager.instance.ShowResultPopup();
    }

    IEnumerator UpdateSurvivalTimeCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => isPlaying);

            yield return updateTimeWait;

            UpdateSurvivalTime();
        }
    }

    private void UpdateSurvivalTime()
    {
        survivalTime += timeUpdateInterval;

        SurvivalTimeUpdated?.Invoke(survivalTime);
    }
}
