using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot_Powerup : MonoBehaviour
{
    //variable definitions
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID;
    private Audio_Manager _audioManager;
    void Start()
    {
        //find audio manager component for script communication
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
        //null check for audio manager component retrieval
        if (_audioManager == null)
        {
        Debug.LogError("The audio manager is NULL");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when it leaves the screen at the bottom, destroy us
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }
    //on trigger collision
    //collision logic
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                if(_powerupID == 0) //this is the triple shot powerup
                { 
                    player.TripleShotActive();
                _audioManager.PlayPowerupSound();
                }
                else if(_powerupID == 1) //this is the speed boost powerup
                {
                    player.SpeedUp();
                _audioManager.PlayPowerupSound();
            }
                else if(_powerupID ==2) //this is the shield powerup
                {
                    player.ShieldActive();
                _audioManager.PlayPowerupSound();
            }
            }
            //when collision with player occurs
            //destroy us
            Destroy(this.gameObject);

        }
    }
}
