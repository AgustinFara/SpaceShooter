using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotateSpeed = 10;
    private float _boundDown = -5.46f;
    private float _boundUp = 7f;  //Un poco mas que boundUp para no eliminarlo al instanciarlo por el spawn Manager
    private float _range = 9f;

    private bool _firstCollision = true;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * 0.5f * Time.deltaTime,Space.World);
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);


        if (transform.position.y <= _boundDown || 
            transform.position.y >= _boundUp   || 
            transform.position.x <= -_range    || 
            transform.position.x >= _range)
        {
            Destroy(this.gameObject,1.0f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if ((other.gameObject.tag == "Player") && (_firstCollision == true))
        {   
            _firstCollision = false;
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                player.DamagePlayer();
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
     {


        if (other.gameObject.tag == "Laser")
         {
            Destroy(other.gameObject, 1f);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

         }
     }

}
