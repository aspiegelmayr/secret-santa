using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private GameObject _doorText;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    void Start()
    {
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSE);
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    public void SeeDoorOption(bool active)
    {
        _doorText.gameObject.SetActive(active);
    }
}
