using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUp : MonoBehaviour
{

    //float
    [SerializeField]
    private float _speed = 3.0f; // velocidad de caida PowerUp

    private float _boundDown = -5.46f; //limite inferior pantalla
    private float _delayPowerUpDestroy = 1f; //variable para retrasar la destruccion del objeto y permitir que se reproduzca el sonido
    
    //integer
    [SerializeField]
    private int _powerUpId; //definidos en el inspector, 0=TripleShoot, 1=Speed, 2 = Shields

    private Player _player; 

    // Update is called once per frame (movimiento de los PowerUps)
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _boundDown)
        {
            Destroy(this.gameObject,_delayPowerUpDestroy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Colision detectada
    {
        if (other.tag == "Player") //si el otro es el Player
        {
            PlaySound(); // Reproduzco el sonido

            _player = other.transform.GetComponent<Player>(); //busco componente Player

            CallPlayerPowerUpMethod();

            this.gameObject.GetComponent<Renderer>().enabled = false;  //deshabilito la visualizacion del objeto
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false; //deshabilito el collider
            Destroy(this.gameObject,_delayPowerUpDestroy); //destruyo el objeto con retardo(para dejar reproducir el sonido)
        }

    }

    private void PlaySound()
    {
        AudioSource audioSource = this.transform.GetComponent<AudioSource>(); //busco Componente

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("PowerUp.PlaySound");
        }
    }

    private void CallPlayerPowerUpMethod()//Llamo al metodo correspondiente al powerup
    {

        if (_player != null)
        {

            _player.AddScore(25);

            switch (_powerUpId)
            {
                case 0:
                    _player.EnableTripleShoot();
                    break;
                case 1:
                    _player.EnableSpeedPowerUp();
                    break;
                case 2:
                    _player.EnableShieldsPowerUp();
                    break;
                default:
                    Debug.Log("PowerUp.CallPlayerPowerUpMethod(default)");
                    break;
            }
        }
        else
        {
            Debug.Log("PowerUp.CallPlayerPowerUpMethod(null)");
        }

    }

}
