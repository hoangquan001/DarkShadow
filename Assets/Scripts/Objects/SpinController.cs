using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] GameObject ExplosionVFX;
    private int direct = 1;
    float force = 1000;
    float lifeTime = 10f;
    float lifeTimer = 0;
    float damage = 10f;

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(force * direct, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Destroy(this.gameObject);
       // Instantiate(ExplosionVFX, transform.position, Quaternion.identity);


        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.takeDamage(damage);


        }
    }
    public void setDirect(int direct)
    {
        this.direct = direct;
    }


    public void addForce(float force)
    {
        this.force = force;
    }
    // Update is called once per frame
    void Update()
    {



        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            Destroy(this.gameObject);
        }

    }
}
