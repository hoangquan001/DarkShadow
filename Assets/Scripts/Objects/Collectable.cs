using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int scoreOfObject;
    BoxCollider2D box2d;

    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0, Vector3.up, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<PlayerCharacter>().addScore(scoreOfObject);
            Destroy(this.gameObject);
        }
    }

    //IEnumerator DelayDesTroy()
    //{
    //    yield return new WaitForSeconds(0.2f);

    //}

}
