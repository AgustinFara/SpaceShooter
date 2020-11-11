using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _gameRestartText;
    [SerializeField]
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
        {
        _scoreText.text = 0.ToString("D6");
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("The _gameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayScore(int playerScore)
    {
        //   _score = _score + 10;
        _scoreText.text = playerScore.ToString("D6") ;
    }

    public void UpdateLives(int playerLives)
    {
        _LivesImage.sprite = _livesSprites[playerLives];

        if (playerLives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _gameRestartText.gameObject.SetActive(true);
        StartCoroutine(FlickGameOver());
        _gameManager.GameOver();

    }

    IEnumerator FlickGameOver()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.3f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.3f);
        }
    }
}
