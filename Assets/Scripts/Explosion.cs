using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDestroy()); 

    }
    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2.38f);
        Destroy(gameObject);
    }
}
