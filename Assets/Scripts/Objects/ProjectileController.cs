using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ExplosionVFX;
    Rigidbody2D rb2d;
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

        rb2d =GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(force * direct,0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);


        if (collision.gameObject.layer == LayerMask.NameToLayer("Monsters"))
        {
            IMonsters monsters = collision.gameObject.GetComponent<IMonsters>();
            monsters.takeDamage(damage);
            monsters.setDirect(-direct);

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
