using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //GameObjects
    [SerializeField]
    private GameObject _enemyPrefab; //Variable Gameobject para agregar el prefab del enemigo

    [SerializeField]
    private GameObject[] _PowerUpPrefab; //Variable Gameobject para agregar el prefab de los powerups

    [SerializeField]
    private GameObject _AsteroidPrefab; //Variable Gameobject para agregar el prefab de los asteroides

    [SerializeField]
    private GameObject _enemyContainer; //Gameobject Auxiliar para Contener Los distintos enemigos Spawneados

    [SerializeField]
    private GameObject _bossPrefab; //Gameobject Auxiliar para Contener Los distintos enemigos Spawneados

    private CameraShake _camera;


    //float
    private float _boundUp = 6.92f; //variable que contiene el limite superior de la pantalla
    private float _range = 9f;  // Rango +- de los lados de la pantalla (donde spawnearan los enemigos)

    //integer
    private int _randomPowerUp; //variable para randomizar el powerup

    //bool
    private bool _stopSpawning = false; //Variable Bool para setear el fin del Spawn 


    // Start is called before the first frame update (llama a las 2 corutinas)
    void Start()
    {

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(CountDowntoBoss());

    }

    IEnumerator SpawnEnemy() //Genera un enemigo cada 5 segundos
    {
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-_range, _range), _boundUp, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; //Mete elnuevo enemigo en el contenedo enemyContainer
            yield return new WaitForSeconds(5f); //espera 5 segundos
        }
    }

    IEnumerator SpawnPowerUp()  //Genera un powerup cada (6-10) segundos
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(6f, 10f)); //en float el max es inclusivo, espera el rango de tiempo
            _randomPowerUp = (Random.Range(0, 3)); //selecciona Powerup Random
            GameObject newPowerUp = Instantiate(_PowerUpPrefab[_randomPowerUp], transform.position + new Vector3(Random.Range(-_range, _range), _boundUp, 0), Quaternion.identity); //instancia el PowerUp

        }
    }

    IEnumerator SpawnAsteroid()  //Genera un Asteroide cada 10 segundos
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10f); //espera 10 segundos
            GameObject newPowerUp = Instantiate(_AsteroidPrefab, transform.position + new Vector3(Random.Range(-_range, _range), _boundUp, 0), Quaternion.identity); //instancia el Asteroide

        }
    }

    public void OnPlayerDeath() //activa el bool para dejar de spawnear, public para que pueda ser llamado por el player
    {   
        _stopSpawning = true;
    }

    IEnumerator CountDowntoBoss()  //Genera un Asteroide cada 10 segundos
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10f); //espera 2 minutos
            _stopSpawning = true;
            yield return new WaitForSeconds(6f); //espera 2 minutos
            GameObject newBoss = Instantiate(_bossPrefab, transform.position + new Vector3(0,15,0), Quaternion.identity); //instancia el Boss
            _camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
            if (_camera != null)
            {
                _camera.Shake(3f);
            }
            else
            {
                Debug.Log("SpawnManager.CountDowntoBoss");
            }
        }
    }


}