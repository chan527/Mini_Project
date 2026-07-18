using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultTimeText;
    [SerializeField] private Button goToMainBtn;

    void Start()
    {
        goToMainBtn.onClick.AddListener(OnClickGoToMainBtn);
    }

    private void OnEnable()
    {
        SetResultTimeText();
    }

    private void SetResultTimeText()
    {
        int resultTime = GameManager.instance.SurvivalTime;
        int minutes = resultTime / 60;
        int seconds = resultTime % 60;

        resultTimeText.text = $"{minutes:00} : {seconds:00}";
    }

    private void OnClickGoToMainBtn()
    {
        UIManager.instance.CloseResultPopup();

        Time.timeScale = 1f;

        // ∏ﬁ¿Œ æ¿ ¿Ãµø
    }
}
