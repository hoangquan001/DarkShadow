using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XProjectile : XObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject ExplosionVFX;
    XObject host;
    public Collider2D _collider;
    public Rigidbody2D rb2d;
    Vector2 direct ;
    float damage;
    public override void InitComponent()
    {
        base.InitComponent();
        rb2d = GetComponent<Rigidbody2D>();

    }
    public virtual void Fire(XObject host, Vector3 pos , float speed, Vector2 direct, int damage)
    {
        this.host = host;
        this.direct = direct;
        this.damage = damage;
        transform.position = pos;
        rb2d.AddForce(direct * speed, ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direct.y, direct.x)*180/Mathf.PI, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        // if (collision.gameObject.layer == LayerMask.NameToLayer("Monsters"))
        // {
        var monsters = collision.gameObject.GetComponent<XEntity>();
        //     monsters.takeDamage(damage);
        //     monsters.setDirect(-direct);
        // }
    }

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

}
