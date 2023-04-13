using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Damageable : MonoBehaviour,IMonsters
{
    public float HealthPoint =100;
    public virtual void takeDamage(float damage)
    {
        HealthPoint -= damage;
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (HealthPoint <= 0)
        {
            DestroyObject();
        }
    }
         

}
