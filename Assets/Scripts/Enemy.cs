using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Properties")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Projectiles and Explosions")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosion;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("SFX")]
    [SerializeField] List<AudioClip> projectileSound;
    [SerializeField] [Range(0, 1)] float projectileSoundVolume = 0.1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        var laser = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        PlayProjectileSFX();
    }

    private void PlayProjectileSFX()
    {
        if (projectileSound.Count > 0)
        {
            var randomProjectileSound = UnityEngine.Random.Range(0, projectileSound.Count - 1);
            AudioSource.PlayClipAtPoint(projectileSound[randomProjectileSound], Camera.main.transform.position, projectileSoundVolume);
        }
    }

    private void PlayDeathSFX()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        TakeDamage(damageDealer);
    }

    private void TakeDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        var explosionVFX = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        PlayDeathSFX();
        Destroy(explosionVFX, durationOfExplosion);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }
}
