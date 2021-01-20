using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    //variable definition
    [SerializeField]
    private AudioClip _explosionSound;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _powerupSound;
    // Start is called before the first frame update
    void Start()
    {
        //retrieve audio source component for script communication
        _audioSource = GetComponent<AudioSource>();
        //null check for component retrieval
        if (_audioSource == null)
        {
            Debug.LogError("The audio source is NULL");
        }

    }
    //method to play an explosion sound effect
    public void PlayExplosionSound()
    {
       if(_audioSource != null)
        {
            _audioSource.PlayOneShot(_explosionSound);
        }
        
    }
    //method to play a powerup sound effect
    public void PlayPowerupSound()
    {
        if(_audioSource != null)
        {
            _audioSource.PlayOneShot(_powerupSound);
        }
    }
}
