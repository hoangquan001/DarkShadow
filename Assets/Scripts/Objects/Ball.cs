using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    float time = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        time += Time.deltaTime;
        print(Mathf.Sin(time));
    }
}
