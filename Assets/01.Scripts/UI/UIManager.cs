using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject resultPopup;
    [SerializeField] private GameObject settingsPopup;

    [SerializeField] private GameObject dim;

    private int depth = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ShowResultPopup()
    {
        resultPopup.SetActive(true);
        dim.SetActive(true);

        depth++;
    }

    public void CloseResultPopup()
    {
        resultPopup.SetActive(false);
        ReduceDepth();
    }

    public void ReduceDepth()
    {
        depth--;

        if (depth == 0)
            dim.SetActive(false);
    }
}
