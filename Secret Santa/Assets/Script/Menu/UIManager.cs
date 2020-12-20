using System.Collections;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private GameObject _doorText;
    public GameObject[] KizuneTexte;
    
    public Events.EventFadeComplete OnMainMenuFadeComplete;
    private int textCount;

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
        
        GameManager.Instance.ToggleKizune();
    }
}
