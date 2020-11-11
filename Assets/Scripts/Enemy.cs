using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private Player _player;
    private Animator _myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        //falta cheqnull
        _myAnimator = GetComponent<Animator>();
        //falta cheq null
        StartCoroutine(FireLaser());
    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float _boundDown = -5.46f;
        float _boundUp = 6.92f;
        float _range = 9f;
        if (transform.position.y <= _boundDown)
        {
            transform.position = new Vector3(Random.Range(-_range, _range), _boundUp, transform.position.z);
        }
         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
            {
            
            Player player = other.transform.GetComponent<Player>();             
              if (player != null)
              {
                player.DamagePlayer();
              }
            GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,0.3f);
        }   
        
        if (other.tag == "Laser")
        {   
            Destroy(other.gameObject,1f);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,0.3f);


            Player player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.AddScore(10);
            }
        }
    }

    IEnumerator FireLaser()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 1.5f));
            GameObject newEnemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }

}
