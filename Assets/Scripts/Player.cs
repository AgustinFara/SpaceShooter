using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private  float _speed = 7f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private GameObject _ShieldPrefab;
    [SerializeField]
    private float _fireRate = 0.05f;
    private float _canFire = -0.2f;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private int _score;

    private bool _speedPowerUpActive = false;
    private bool _tripleShootActive = false;
    private bool _shieldPowerUpActive = false;

    private Canvas _canvas;


    // Start is called before the first frame update
    void Start()
    {
        // Setea el Gameobject al centro (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        if (_canvas == null)
        {
            Debug.LogError("The Canvas is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMovement();

        SetBounds();

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire)
        {
            if (_tripleShootActive == true)
            {
                FireTripleShoot();
            }
            else
            {
                FireLaser();
            }
        }
    }
    
    void PlayerMovement()
    {
        // Setea movimiento por los ejes XY

       // float xInput = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxis("Horizontal");
       // float yInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right* xInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up* yInput * _speed * Time.deltaTime);
    }

    void SetBounds()
    {
        // Setea Bordes
        float _boundDown = -3.5f;
        float _boundUp = 0f;
        float _boundLeft = -11.3f;
        float _boundRight = 11.3f;

        if (transform.position.y >= _boundUp)
        {
            transform.position = new Vector3(transform.position.x, _boundUp, transform.position.z);
        }
        else if (transform.position.y <= _boundDown)
        {
            transform.position = new Vector3(transform.position.x, _boundDown, transform.position.z);
        }

            
        if (transform.position.x >= _boundRight)
        {
            transform.position = new Vector3(_boundLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= _boundLeft)
        {
            transform.position = new Vector3(_boundRight, transform.position.y, transform.position.z);
        }
    }
    void FireLaser()
    {
        float _offsetValue = 0.8f;

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, _offsetValue, 0), Quaternion.identity);

    }

    void FireTripleShoot()
    {

        _canFire = Time.time + _fireRate;
        Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);

    }

    public void DamagePlayer()
    {
        if (_shieldPowerUpActive == true)
        {
            _shieldPowerUpActive = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            return;

        }

        _lives --;

        gameObject.transform.GetChild(Random.Range(2, 4)).gameObject.SetActive(true);

        if (_lives == 1)
        { 
            if (gameObject.transform.GetChild(2).gameObject.activeSelf == true)
            {
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if(gameObject.transform.GetChild(2).gameObject.activeSelf == false)
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        _canvas.UpdateLives(_lives);

        if( _lives < 1)
        {
            GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject,0.3f);

            SpawnManager spawnManager = _spawnManager.transform.GetComponent<SpawnManager>();

            if (spawnManager != null)
            {
                spawnManager.OnPlayerDeath();
            }
        }
    }
    public void EnableTripleShoot()
    {

        _tripleShootActive = true;
        StartCoroutine(PowerDownTripleShoot());
 
    }

    IEnumerator PowerDownTripleShoot()
    {
        while (_tripleShootActive == true)
        {
            yield return new WaitForSeconds(5f);
            _tripleShootActive = false;
        }
    }

    public void EnableSpeedPowerUp()
    {
        if (_speedPowerUpActive == false)
        {
            _speed = _speed * 2;
            _speedPowerUpActive = true;
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
        }

        StartCoroutine(PowerDownSpeedPowerUp());
        

    }

    IEnumerator PowerDownSpeedPowerUp()
    {
        while (_speedPowerUpActive == true)
        { 
        yield return new WaitForSeconds(5f);
            if(_speedPowerUpActive == true)
            {
                _speed = _speed / 2;
                _speedPowerUpActive = false;
                gameObject.transform.GetChild(4).gameObject.SetActive(false);
                gameObject.transform.GetChild(5).gameObject.SetActive(false);
            }
        }   
    }
    public void EnableShieldsPowerUp()
    {
        if (_shieldPowerUpActive == false)
        {
            _shieldPowerUpActive = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    public void AddScore(int points)
    {
        _score = _score + points;
        _canvas.DisplayScore(_score);

    }


}
