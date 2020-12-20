using System.Collections;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private DeathScreen _deathScreen;

    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private GameObject _doorText;
    [SerializeField] private GameObject _needText;
    [SerializeField] private GameObject _wonText;
    public GameObject[] KizuneTexte;
    
    public Events.EventFadeComplete OnMainMenuFadeComplete;
    public Events.EventDeath HandleDeath;
    private int textCount;

    void Start()
    {
        _deathScreen.HandleDeath.AddListener(OnDeath);
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

    void OnDeath(bool tru)
    {
        HandleDeath.Invoke(true);
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    public void SeeDoorOption(bool active)
    {
        _doorText.gameObject.SetActive(active);
    }
    
    public void KizuneTalk(int howMuch)
    {
        StartCoroutine(Kizune(howMuch));
    }

    IEnumerator Kizune(int howMuch)
    {
        int textCStart = textCount;
        GameManager.Instance.ToggleKizune();

        for (int i = textCount; i < textCStart + howMuch; i++, textCount++)
        {
            KizuneTexte[textCount].SetActive(true);
            yield return new WaitForSeconds(5.0f);
            KizuneTexte[textCount].SetActive(false);
        }

        GameManager.Instance.hasKizune = true;
        GameManager.Instance.ToggleKizune();
    }

    public void SetNeed(bool active)
    {
        _needText.gameObject.SetActive(active);
    }

    public void SetWon(bool active)
    {
        _wonText.gameObject.SetActive(active);
    }

    public void StartDeath()
    {
        _deathScreen.FadeIn();
    }

    public void StopDeath()
    {
        _deathScreen.StopDeath();
    }
}
