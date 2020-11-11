using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

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
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        float _boundUp = 7.5f;

        if (transform.position.y >= _boundUp)
        {
            if (transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
                Destroy(this.gameObject);

            }
        }
    }
}
