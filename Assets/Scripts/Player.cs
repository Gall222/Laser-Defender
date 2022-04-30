using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 300;
    

    [Header("Player Sound Effects")]
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float getDamageSoundVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f;
    [SerializeField] AudioClip getDamageSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFirePeriod = 0.1f;

     
    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

 

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
       
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax) ;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
    {
        int damage = damageDealer.GetDamage();
        health -= damage;
        AudioSource.PlayClipAtPoint(getDamageSFX, Camera.main.transform.position, getDamageSoundVolume);

        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().loadGameOver();
        Destroy(gameObject);
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    

    //IEnumerator DeathContinuously()
    //{
    //    yield return new WaitForSeconds(2f);
    //    FindObjectOfType<Level>().loadGameOver();
    //}

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
