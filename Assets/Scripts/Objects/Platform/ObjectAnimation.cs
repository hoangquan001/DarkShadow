using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    MovingObject _moving;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _moving = GetComponent<MovingObject>();
    }

    // Update is called once per frame
    void Update()
    {

        _animator.SetBool("isActive", _moving.IsActive());

    }
}
