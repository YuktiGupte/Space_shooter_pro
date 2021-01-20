using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    //variable definitions
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Vector3 _laserPosition = new Vector3(0, 1.05f, 0);
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isSpeedUpActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score = 0;
    private UI_Manager _uimanager;
    [SerializeField]
    private GameObject[] _engine;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserShot;
    private Audio_Manager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //retrieve spawn manager component for script communication
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //assign starting position to player
        transform.position = new Vector3(0, 0, 0);
        //shield is set to inactive by default
        _shield.SetActive(false);
        //null checks for component retrieval
                if (_spawnManager == null)
                {
            Debug.LogError("The spawn manager is NULL");
                }
        //retrieve UI manager component for script communication
        _uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        //null checks for component retrieval
        if (_uimanager == null)
                {
            Debug.LogError("The UI Manager is NULL");
                }
        //engine damage animation objects are set to false at the start of the game
        _engine[0].SetActive(false);
        _engine[1].SetActive(false);
        //retrieve audio source component for script communication
        AudioSource _audioSource = GetComponent<AudioSource>();
        //null checks for component retrieval
        if (_audioSource == null)
        {
            Debug.LogError("The audio source on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserShot; //it will play the sound of a laser being fired
        }
        //retrieve audio manager component for script communication
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
        //null checks for component retrieval
        if (_audioManager ==null)
        {
            Debug.LogError("The audio manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //pressing the space key is the command for firing the laser
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) //laser has a cooldown hence time.time is being used
        { 
            fireLaser(); 
        }
            
    }
    void fireLaser()
    {
       
        {
            _canFire = Time.time + _fireRate; //defining cooldown behaviour
            if (_isTripleShotActive == true && Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity); //triple shot allows us to fire 3 laser beams simultaneously but only when we have collected the powerup
            }
            else if (_isTripleShotActive == false && Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_laserPrefab, transform.position + _laserPosition, Quaternion.identity);
            }
            //play audio clip
           _audioSource.Play(0);
        }
    }
    void CalculateMovement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        if(_isSpeedUpActive == true) //speed up powerup, when collected, speeds up the player
        {
            SpeedUp(); 
        }
        //moves player around as per user input
        transform.Translate(Vector3.right * _speed * horizontalinput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalinput * Time.deltaTime);
        //defining wrapping behaviour of player
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    public void Damage ()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            //turn off shield visual
            _shield.SetActive(false); //the shield protects the player from damage but for only one hit
            return;
        }
        _lives--; //one hit takes one life from the player
        _audioManager.PlayExplosionSound();
        if(_lives == 2)
        {
            _engine[0].SetActive(true); //engine damage animation
        }
        else if (_lives == 1)
        {
            _engine[1].SetActive(true); //engine damage animation
        }
        _uimanager.LivesUpdate(_lives); //reflect the change on the display
                
        if (_lives < 1)
        {
            Destroy(this.gameObject); //remove the player from the field
            _audioManager.PlayExplosionSound(); //an explosion sound effect
            _spawnManager.OnPlayerDeath(); //enemies and powerups should stop spawning
            
        }
    }
    //triple shot powerup behaviour
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown()); //triple shot remains active only for a little while
    }
    IEnumerator TripleShotPowerDown()
    {
        //wait 5s
       
        yield return new WaitForSeconds(5.0f);
        //set istripleshotactive to false
        _isTripleShotActive = false;
    }
    //speed up powerup behaviour
    public void SpeedUp()
    {
        _isSpeedUpActive = true;
        _speed = 7.0f;
        StartCoroutine(SpeedUpPowerDown()); //speed up remains active only for a little while
    }
    IEnumerator SpeedUpPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedUpActive = false;
        _speed = 3.5f;
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
       //turn on shield visualiser
        _shield.SetActive(true);
        
    }
    //method for score update - calculates player score
    public void UpdateScore()
    {
        _score = _score + 10;
        _uimanager.ShowScore(_score);
    }
}
