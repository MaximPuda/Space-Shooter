using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _engineParticles;
    [SerializeField] private ParticleSystem _crashParticles;

    private SpriteRenderer _spiteRenderer;
    
    private void Awake()
    {
        _spiteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GlobalEventManager.OnPlayerDie += DieFx;
        GlobalEventManager.OnPlayerTakeDamge += TakeDamageFx;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPlayerDie -= DieFx;
        GlobalEventManager.OnPlayerTakeDamge -= TakeDamageFx;
    }

    public void TakeDamageFx(int currentHealth)
    {
        _crashParticles.Play();
    }

    public void DieFx()
    {
        _spiteRenderer.enabled = false;
        _engineParticles.Stop();
        _crashParticles.Play();
    }
}
