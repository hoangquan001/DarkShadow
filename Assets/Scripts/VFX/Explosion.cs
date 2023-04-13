using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    //bool isPlayVFX = false;
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ParticleSystem>().isStopped)
        {

            Destroy(gameObject);
        }
       

    }

}
