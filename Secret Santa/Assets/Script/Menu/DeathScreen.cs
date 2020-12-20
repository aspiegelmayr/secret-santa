using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public Events.EventDeath HandleDeath;
    [SerializeField] private Animation _deathScreenAnimator;
    [SerializeField] private AnimationClip _fadeInAnimation;

    public void OnDeath()
    {
        HandleDeath.Invoke(true);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        _deathScreenAnimator.Stop();
        _deathScreenAnimator.clip = _fadeInAnimation;
        _deathScreenAnimator.Play();
    }

    public void StopDeath()
    {
        gameObject.SetActive(false);

        _deathScreenAnimator.Stop();

    }
}
