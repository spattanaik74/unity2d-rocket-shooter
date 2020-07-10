using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Configuration parameters

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip onPlayerShot;
    [SerializeField] [Range(0, 1)] float onPlayerShotVolume = 0.5f;
    [SerializeField] AudioClip onDeath;
    [SerializeField] [Range(0, 1)] float onDeathVolume = 1f; 

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float projectileSpeed = 0.1f;

    //state
    Coroutine firingCoroutine;


    float minX, maxX;
    float minY, maxY;
    

    // Start is called before the first frame update
    void Start()
    {
        SetUpBound();
        
    }

    private void SetUpBound()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        PlayerShoot();

    }


    private void PlayerShoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           firingCoroutine =  StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    
    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(onPlayerShot, Camera.main.transform.position, onPlayerShotVolume);

            yield return new WaitForSeconds(projectileSpeed);
            
        }
    }



    private void MovePlayer()
    {
        var deltaXpos = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaYpos = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaXpos ,minX , maxX );
        var newYpos = Mathf.Clamp(transform.position.y + deltaYpos, minY, maxY);
        transform.position = new Vector2(newXpos, newYpos);

   
    }

    private void OnTriggerEnter2D(Collider2D enemyLaser)
    {
        Damage laser = enemyLaser.gameObject.GetComponent<Damage>();
        if (!laser) { return; }
        ProcessHitIn(laser);
    }

    private void ProcessHitIn(Damage laser)
    {
        health -= laser.Getdamage();
        laser.Hit();
        if (health <= 0)
        {
            Death();
            
        }
    }

    private void Death()
    {
        
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(onDeath, Camera.main.transform.position, onDeathVolume);
        FindObjectOfType<Level>().LoadGameOver();
        
    }
}
