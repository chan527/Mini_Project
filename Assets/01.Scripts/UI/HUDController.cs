using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI survivalTimeText;

    private void OnEnable()
    {
        GameManager.instance.SurvivalTimeUpdated += UpdateTimeText;
    }

    private void OnDisable()
    {
        GameManager.instance.SurvivalTimeUpdated -= UpdateTimeText;
    }

    private void UpdateTimeText(int survivalTime)
    {
        int minutes = survivalTime / 60;
        int seconds = survivalTime % 60;

        survivalTimeText.text = $"{minutes:00} : {seconds:00}";
    }
}
