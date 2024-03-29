using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem destroyedEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayEffect(hitEffect);
            ShakeCamera();
            audioPlayer.PlayDamageClip();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damageDelt)
    {
        this.health -= damageDelt;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        } else 
        {
            this.levelManager.LoadGameOver();
        }
        PlayEffect(destroyedEffect);
        Destroy(gameObject);
    }

    void PlayEffect(ParticleSystem effect)
    {
        if(effect != null)
        {
            ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(this.cameraShake != null && applyCameraShake)
        {
            this.cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
