
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float attackCD=0.5f;
    [SerializeField] private float attackTimer=0;
    [SerializeField] private float damage=5;
    LayerMask playerLayer;
    private Collider2D _boxCollider;
    private GameObject player;
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        _boxCollider = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        attackTimer = attackTimer<0?-Time.deltaTime: attackTimer- Time.deltaTime;
        if (_boxCollider.IsTouchingLayers(playerLayer))
        {
            
            if (attackTimer < 0)
            {
                player.GetComponent<PlayerCharacter>().takeDamage(damage);
                attackTimer = attackCD;
            }
        }
    }
    // Update is called once per frame

   



}
