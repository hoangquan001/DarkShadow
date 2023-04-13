using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController _playerController;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();  

    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("Ground", _playerController.isGround);
        playerAnimator.SetBool("Fall", _playerController.isFalling);
        playerAnimator.SetBool("Jump", _playerController.isJumping);
        playerAnimator.SetFloat("SpeedRun", Mathf.Abs( playerRigidbody.velocity.x));
        playerAnimator.SetInteger("SkillState", (int)_playerController.curSkill);
        
    }
}
