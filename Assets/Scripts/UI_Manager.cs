using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    //variable definitions
    [SerializeField]
    private Text _scoretext;
    private Player _player;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameovertext;
    [SerializeField]
    private Text _restartLevelText;
    private GameManager _gameManager;
  
    // Start is called before the first frame update
    void Start()
    {
       //assign score to 0 at start
        _scoretext.text = "Score: " + 0;
        //retrieve player component for script communication
        _player = GameObject.Find("Player").GetComponent<Player>();
        //make the "game over" text invisible at the start
        _gameovertext.gameObject.SetActive(false);
        //make the "press R to restart text invisible on start
        _restartLevelText.gameObject.SetActive(false);
        //retrieve game manager component for script communication
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        //null check for game manager and player components retrieval
        if(__player == null)
        {
            Debug.LogError("The player is NULL");
        }
        if (_gameManager == null)
        {
            Debug.LogError("The game manager is NULL");
        }
    }

    //method to display the current score on the screen
    public void ShowScore(int playerScore)
    {
        //communicate with UI from player method to update score
        _scoretext.text = "Score: " + playerScore.ToString();
    }
    //method to update the graphic showing current lives held by player. ends game if no lives remain
    public void LivesUpdate(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if(currentLives ==0)
        {
            GameOverSequence();
        }
    }
    //displays game over text and instructs player to press R to restart the level
    void GameOverSequence()
    {
        StartCoroutine(FlickerText());
        _restartLevelText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
    //coroutine to define the flickering behaviour of the game over text
    IEnumerator FlickerText()
    {
        while (true)
        {
            _gameovertext.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameovertext.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
