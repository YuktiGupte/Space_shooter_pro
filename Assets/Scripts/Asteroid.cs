using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //variable definitions
    private float _speed = 10.0f;
    private Player _player;
    private Animator _animator;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private Audio_Manager _audioManager;
  
   
    // Start is called before the first frame update
    void Start()
    {
        //component retrieval for script communication
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //null check for component retrieval
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
        }
        //component retrieval for script communication
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
        //null check for component retrieval
        if (_audioManager == null)
        {
            Debug.LogError("The audio manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,20), _speed * Time.deltaTime); //to make the asteroid rotate slowly
    }
    //makes the enemies and powerups start spawning once the asteroid has been destroyed
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioManager.PlayExplosionSound();
            Destroy(this.gameObject);
            _spawnManager.StartSpawning();
           
        }
    }
}
