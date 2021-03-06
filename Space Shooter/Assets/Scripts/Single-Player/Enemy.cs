using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;

    [SerializeField]
    Animator explosionAnimation;

    AudioManager _audioManager;
    AudioSource audioManagerAudioSource;

    [SerializeField] float _speed = 4f;

    [SerializeField] GameObject _enemyLaserPrefab;
    [SerializeField] float _fireRate = 2.5f;
    float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        FireLaser();
        _audioManager = FindObjectOfType<AudioManager>();
        audioManagerAudioSource = _audioManager.GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        explosionAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.5f)
        {
            float randomXPos = Random.Range(-9.6f, 9.6f);
            transform.position = new Vector3(randomXPos, 6.35f, 0);
            FireLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            explosionAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.8f);
            audioManagerAudioSource.PlayOneShot(_audioManager.explosionSound, _audioManager.explosionSoundVoulme);
        }

        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);
            player.AddToScore();
            explosionAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1.8f);
            audioManagerAudioSource.PlayOneShot(_audioManager.explosionSound, _audioManager.explosionSoundVoulme);
        }
    }

    void FireLaser()
    {
        StartCoroutine(FireLaserDelay());
        
    }

    IEnumerator FireLaserDelay()
    {
       if (Time.time > _canFire)
       {
         _canFire = Time.time + _fireRate;
         Vector3 posToSpawn = new Vector3(-0.15f, -1.4f, 0);
         yield return new WaitForSeconds(0.25f);
         Instantiate(_enemyLaserPrefab, transform.position + posToSpawn, Quaternion.identity);
         audioManagerAudioSource.PlayOneShot(_audioManager.enemyLaserSound, _audioManager.enemyLaserSoundVolume);
       }
    }
}
