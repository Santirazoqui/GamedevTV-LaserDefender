using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem destroyedEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayEffect(hitEffect);
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damageDelt)
    {
        this.health -= damageDelt;
        if(health <= 0)
        {
            PlayEffect(destroyedEffect);
            Destroy(gameObject);
        }
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
}
