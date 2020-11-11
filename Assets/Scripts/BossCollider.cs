using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    private Player _player;
    private Boss _boss;
    private bool _disableHitbox = false;
    [SerializeField]
    private GameObject _explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();

        if (_boss == null)
        {
            Debug.Log("Error");
        }

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.Log("Error");
        }
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {
            //Damage Boss
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _disableHitbox = true;

            _boss.Damage();
            _player.AddScore(10);
            StartCoroutine(DisableHitbox());

        }
    }

    IEnumerator DisableHitbox()

    {
        while(_disableHitbox == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1.2f);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            _disableHitbox = false;
        }

    }
}
