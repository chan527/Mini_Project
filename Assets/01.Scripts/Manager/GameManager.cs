using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float survivalTime;
    private float timeUpdateInterval;

    private bool isPlaying;

    private WaitUntil waitUntilPlaying;
    private WaitForSeconds updateTimeWait;

    private Coroutine timeUpdateRoutine;

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
        survivalTime = 0f;
        timeUpdateInterval = 0.1f;

        isPlaying = false;

        waitUntilPlaying = new WaitUntil(() => isPlaying);
        updateTimeWait = new WaitForSeconds(timeUpdateInterval);
    }

    public void GameStart()
    {
        survivalTime = 0f;

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

        //UIManager.instance.ShowResultUI();
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

        //UIManager.instace.UpdateSurvivalTimeUI(survivalTime);
    }
}
