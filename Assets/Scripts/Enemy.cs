using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    [SerializeField] int health = 100;
    [SerializeField] int points = 10;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = -10f;
    [SerializeField] float duractionOfExplosion = 1f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] [Range(0,1)]float deathSoundVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;


    // Start is called before the first frame update
    void Start()
    {
        reloadShotCounter();
    }

    private void reloadShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            reloadShotCounter();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
    }

   

    public void getDamage(int damage)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
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
        FindObjectOfType<GameSession>().UpdateScore(points);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(explosion, duractionOfExplosion);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }
}
