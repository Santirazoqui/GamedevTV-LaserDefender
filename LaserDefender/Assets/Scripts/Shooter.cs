using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 2f;

    public bool isFiring;
    Coroutine firingCoroutine;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(this.isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        } 
        else if (!this.isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(this.projectilePrefab, 
                        transform.position,
                        Quaternion.identity
                        );
            Rigidbody2D laserRigidbody = laser.GetComponent<Rigidbody2D>();
            if(laserRigidbody != null)
            {
                laserRigidbody.velocity = transform.up * this.projectileSpeed;
            }
            Destroy(laser, this.projectileLifetime);
            yield return new WaitForSeconds(this.fireRate);
        }
    }
}
