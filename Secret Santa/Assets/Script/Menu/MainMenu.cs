using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeInAnimation;
    [SerializeField] private AnimationClip _fadeOutAnimation;

    public Events.EventFadeComplete OnMainMenuFadComplete;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {
        OnMainMenuFadComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
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
        UIManager.Instance.SetDummyCameraActive(false);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }
}
