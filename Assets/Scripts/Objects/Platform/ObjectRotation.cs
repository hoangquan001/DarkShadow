using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float roundPerSecond = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0,0,1), roundPerSecond * 180 * Time.fixedDeltaTime);
    }
}
