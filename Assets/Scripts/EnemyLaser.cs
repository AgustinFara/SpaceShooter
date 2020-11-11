using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private int _speed = 8;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMovement();  
    }

    void LaserMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float _boundDown = -5.5f;

        if (transform.position.y <= _boundDown)
        {
            Destroy(this.gameObject);
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
            Destroy(this.gameObject, 1f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject, 1f);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject, 1f);

            Player player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.AddScore(20);
            }
        }
    }

}
