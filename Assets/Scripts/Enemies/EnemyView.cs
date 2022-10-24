using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _explosion;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _explosion = GetComponentInChildren<ParticleSystem>();
    }

    public void Die()
    {
        if(_spriteRenderer != null)
            _spriteRenderer.enabled = false;
        
        if(_explosion != null)
            _explosion.Play();
    }
}
