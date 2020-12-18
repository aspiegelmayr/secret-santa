using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    

    [SerializeField] private Button StartBotton;
    [SerializeField] private Button QuitBotton;
    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeInAnimation;
    [SerializeField] private AnimationClip _fadeOutAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        
        StartBotton.onClick.AddListener(HandleStartClicked);
        QuitBotton.onClick.AddListener(HandleQuitClicked);
    }

    public void OnFadeOutComplete()
    {
        OnMainMenuFadeComplete.Invoke(true);
        gameObject.SetActive(false);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    void HandleStartClicked()
    {
        GameManager.Instance.StartGame();
    }

    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        UIManager.Instance.SetDummyCameraActive(true);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }
}
