using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DustVFX;
    public GameObject ExplosionVFX;
    public GameObject TrailVFX;
    //public GameObject Dust;

    // private PlayerController playerControl;

    void Start()
    {

        // playerControl = GetComponent<PlayerController>();
    }
    public void playTrailVFX()
    {
        TrailVFX.SetActive(true);
    }
    public void stopTrailVFX()
    {
        TrailVFX.SetActive(false);
    }
    public void  playExplosion(Vector2 position)
    {
        Instantiate(ExplosionVFX, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {


        // if(playerControl.movementState == PlayerMovement.Run)
        // {
        //     DustVFX.SetActive(true);
        // }
        // else
        // {
        //     DustVFX.SetActive(false);
        // }
    }
}
