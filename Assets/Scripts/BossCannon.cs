using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ResizeLaser());
    }

    IEnumerator ResizeLaser()
    {
        while (true)
        {
            this.transform.localScale += new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(0.4f);
        }

    }

    void Fire()
    {
       
    }
}
