using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    //Configuration parameter
    [Header(" Enemy State")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 150;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeShots = 0.2f;
    [SerializeField] float maxTimeShots = 0.3f;

    [Header("Enemy Laser")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float enemyLaserSpeed = 10f;
    [SerializeField] float projectileSpeed = 0.1f;


    [Header("enenmy vfx")]
    [SerializeField] GameObject deathVfx;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip onEnenmyShot;
    [SerializeField] [Range(0, 1)] float onEnemyShotVolume = 0.25f;
    [SerializeField] AudioClip onDestroy;
    [SerializeField] [Range(0,1)] float onDestroyVolume = 0.7f;


    
    void Start()
    {
        shotCounter = Random.Range(minTimeShots, maxTimeShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountAndShoot();
    }
    private void CountAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeShots, maxTimeShots);
        }
    }
    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(onEnenmyShot, Camera.main.transform.position, onEnemyShotVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        Damage damageDealer = other.gameObject.GetComponent<Damage>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(Damage damageDealer)
    {
        health -= damageDealer.Getdamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        GameObject explosion = Instantiate(deathVfx, transform.position, transform.rotation);
        Destroy(explosion , durationOfExplosion);
        AudioSource.PlayClipAtPoint(onDestroy, Camera.main.transform.position,onDestroyVolume);
    }
}
