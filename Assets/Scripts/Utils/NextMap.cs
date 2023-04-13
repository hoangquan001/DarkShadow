using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneController.Instance.NextMap();
        DataPersistanceManager.Instance.nextMap();
    }
}
