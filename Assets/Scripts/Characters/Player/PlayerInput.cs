using System.Collections;
using UnityEngine;


//public interface IPlayerInput 
//{

//    // Use this for initialization
//    public bool getJumpInput();
//    public float moveHorizontal();
//    public float moveVertical();
//    public bool getDashInput();
//    public bool getAttackInput();
//    public bool getMagicFireInput();
//    //{
//    //moveInput.y = Input.GetAxisRaw("Vertical");
//    //    return Input.GetAxisRaw("Horizontal");
//    //}
    

//}


public class PlayerInput
{
    private static PlayerInput instance;
    public static PlayerInput Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = new PlayerInput();
            return instance;
        }
    }

    bool unLock=true;
    bool isDash = true;
    bool isAttack = true;
    bool isMagicFire = true;
    // Use this for initialization
    public bool getJumpUpInput()
    {
        return (unLock && Input.GetKeyUp(KeyCode.UpArrow));
    }
    public bool getJumpDownInput()
    {
        return (unLock && Input.GetKeyDown(KeyCode.UpArrow));
    }

    public bool getKeyF()
    {
        return (Input.GetKeyDown(KeyCode.F));
    }

    public float moveHorizontal()
    {
        if (!unLock) return 0;
        return Input.GetAxisRaw("Horizontal");
    }

    public float moveVertical()
    {
        if (!unLock) return 0;
        return Input.GetAxisRaw("Vertical");
    }
    public bool getDashInput() {
        return (unLock && isDash&&Input.GetKeyDown(KeyCode.C));
    }
    public bool getAttackInput() {
        return (unLock && isAttack && Input.GetKeyDown(KeyCode.X));
    }
    public bool getMagicFireInput()
    {
        return (unLock && isMagicFire && Input.GetKeyDown(KeyCode.V));
    }
    public void setActive(bool value)
    {
        unLock = value;
    }
    public PlayerInput LockDash(bool value)
    {
        isDash = value;
        return this;
    }   
    public PlayerInput LockAttack(bool value)
    {
        isAttack = value;
        return this;
    }   
    public PlayerInput LockMagic(bool value)
    {
        isMagicFire = value;
        return this;
    }
    //{
    //moveInput.y = Input.GetAxisRaw("Vertical");
    //    return Input.GetAxisRaw("Horizontal");
    //}


}
