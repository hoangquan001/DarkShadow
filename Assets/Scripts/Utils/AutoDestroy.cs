using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float existTime = 10;
    private float existTimer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        existTimer += Time.deltaTime;
        if(existTimer> existTime)
        {
            Destroy(this.gameObject);
        }
    }
}
