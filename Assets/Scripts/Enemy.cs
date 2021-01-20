using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variable definition
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;
    private Audio_Manager _audioManager;
   
    // Update is called once per frame
    void Start()
    {
        //component retrieval for script communication
        _player = GameObject.Find("Player").GetComponent<Player>();
        //null check for component retrieval
        if(_player == null)
        {
            Debug.LogError("The player is NULL");
        }
        //component retrieval for script communication
        _animator = GetComponent<Animator>();
        //null check for component retrieval
        if (_animator == null)
        {
            Debug.LogError("The animator is NULL");
        }
        //component retrieval for script communication
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
        //null check for component retrieval
        if (_audioManager == null)
        { 
            Debug.LogError("The audio manager is NULL");
        }
    }
    void Update()
    {
        EnemyMovement();   
    }
    //define enemy movement behaviour
    void EnemyMovement()
    {
        //create enemy
        //Instantiate(this.gameObject, _enemyPosition, Quaternion.identity)
        float enemyPositionX = Random.Range(-8.0f, 8.0f);
        //move down at 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if it reaches the bottom, respawn at top with new random x position
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(enemyPositionX, 5.5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        if (other.tag == "Player")
            {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            { 
                player.Damage(); 
            }
            //damage player
            //destroy us
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0; //to make the collision behaviour smoother
            _audioManager.PlayExplosionSound();
            Destroy(this.gameObject, 2.8f);
            }

        //if other is laser
        //destroy laser
        //destroy us
        else if (other.tag == "Laser")
        {
            if(_player != null)
            {
                //update score
                _player.UpdateScore();
            }
            Destroy(other.gameObject);
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioManager.PlayExplosionSound();
            Destroy(this.gameObject, 2.8f);
            
        }
    }
}
