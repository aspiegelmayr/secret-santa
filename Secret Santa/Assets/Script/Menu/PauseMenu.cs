using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeBotton;
    [SerializeField] private Button RestartBotton;
    [SerializeField] private Button QuitBotton;

    private void Start()
    {
        ResumeBotton.onClick.AddListener(HandleResumeClicked);
        RestartBotton.onClick.AddListener(HandleRestartClicked);
        QuitBotton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
