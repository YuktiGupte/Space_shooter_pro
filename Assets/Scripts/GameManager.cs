using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variable definition
    [SerializeField]
    private bool _isGameOver;
    public void GameOver()
    {
        _isGameOver = true; //to show the game over text
        Debug.Log("GameManager:: GameOver() called");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) //command to restart game
        {
            SceneManager.LoadScene(1); //current game scene
        }
    }
}
