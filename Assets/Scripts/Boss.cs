using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private bool _bossGoesDown = true;
    private bool _bossGoesSides = false;
    private bool _bossIsIdle = false;
    private bool _bossFireCannon = false;
    private bool _bossFireLaser = true;
    private bool _bossRight = false;
    private bool _bossLeft = false;
    private bool _DamageBoss = false;
    private bool _BossIsDead = false;

    [SerializeField]
    private int _damageBar = 0;

    private int _auxRandomizeSide;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _BossLaserPrefab;
    [SerializeField]
    private GameObject _BossExplosion;
    [SerializeField]

    private GameObject _BossCannon;
    private Color _colorPrev;

    // Start is called before the first frame update
    void Start()
    {
        _colorPrev = this.transform.GetComponent<SpriteRenderer>().color;
        StartCoroutine(BossSettleDown());

        StartCoroutine(BossRandomFireRAte());
        StartCoroutine(BossChangeMovement());
        StartCoroutine(BossRandomCannon());

    }

    // Update is called once per frame
    void Update()
    {

        SetBounds();

        if (_bossGoesDown == true)
        {
            this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.down * 0 * Time.deltaTime);
        }


            if (_bossGoesSides == true)
        {



            if (_auxRandomizeSide == 1)
            {
                _bossRight = true;
                this.transform.Translate(Vector3.right * _speed * Time.deltaTime);

            }
            if (_auxRandomizeSide == 2)
            {
                _bossLeft = true;
                this.transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

        }

    }

    public void Damage()
    {
        _damageBar = _damageBar + 1;

        _DamageBoss = true;

        if (_damageBar > 10)
        {

            _BossIsDead = true;
            StartCoroutine(BossDeath());

        }
        else
        {
            StartCoroutine(DamageColorChange());
        }


    }
    void SetBounds()
    {
        // Setea Bordes

        float _boundLeft = -11.3f;
        float _boundRight = 11.3f;

        if (transform.position.x >= _boundRight)
        {
            transform.position = new Vector3(_boundLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= _boundLeft)
        {
            transform.position = new Vector3(_boundRight, transform.position.y, transform.position.z);
        }
    }
    IEnumerator BossSettleDown()
    {
        while (_bossGoesDown == true)
        {
            yield return new WaitForSeconds(2.2f);
            _bossGoesDown = false;
            yield return new WaitForSeconds(1f);
            _bossGoesSides = true;

        }
    }


    IEnumerator BossRandomFireRAte()
    {
        while (true)
        {


            yield return new WaitForSeconds((Random.Range(0.5f, 1.5f)));
            GameObject newEnemyLaser = Instantiate(_BossLaserPrefab, transform.position + new Vector3(-2.6f, -1f, 0), Quaternion.identity);
            GameObject newEnemyLaser2 = Instantiate(_BossLaserPrefab, transform.position + new Vector3(-2.2f, -0.5f, 0), Quaternion.identity);
            GameObject newEnemyLaser3 = Instantiate(_BossLaserPrefab, transform.position + new Vector3(2.2f, -0.5f, 0), Quaternion.identity);
            GameObject newEnemyLaser4 = Instantiate(_BossLaserPrefab, transform.position + new Vector3(2.6f, -1f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }


    IEnumerator BossRandomCannon()
    {
        while (true)
        {

            yield return new WaitForSeconds((Random.Range(3.0f, 5.0f)));
            GameObject newCannon = Instantiate(_BossCannon, transform.position + new Vector3(0, -2f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);

        }
    }

    IEnumerator BossChangeMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds((Random.Range(1.5f, 2.0f)));
            _auxRandomizeSide = (Random.Range(1, 3));
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator DamageColorChange()
    {
        while (_DamageBoss == true)
        {
            this.transform.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.3f);
            this.transform.GetComponent<SpriteRenderer>().color = _colorPrev;
            yield return new WaitForSeconds(0.3f);
            this.transform.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.3f);
            this.transform.GetComponent<SpriteRenderer>().color = _colorPrev;
            yield return new WaitForSeconds(0.3f);
            this.transform.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.3f);
            this.transform.GetComponent<SpriteRenderer>().color = _colorPrev;
            _DamageBoss = false;
        }
    }

    IEnumerator BossDeath()
    {
        while (_BossIsDead == true)
        {
            _speed = 0;
            GameObject newExplosion = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)),(Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion2 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion3 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion4 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion5 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion6 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion7 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion8 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject newExplosion9 = Instantiate(_BossExplosion, transform.position + new Vector3((Random.Range(-4f, 4f)), (Random.Range(-2f, 2f)), 0), Quaternion.identity);
            _DamageBoss = false;
            Destroy(this.gameObject);

        }
    }
}
