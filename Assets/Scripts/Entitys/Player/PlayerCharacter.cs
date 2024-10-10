using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PlayerCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Health and Mana")]
    [SerializeField] private float healthPoint = 100f;
    [SerializeField] private float ManaPoint = 100f;
    [SerializeField] private int score = 0;


    [Header("Attack skill")]
    public float attackCD = 1f;
    [SerializeField] private float attackDamage = 10f;
    public Vector2 size;
    public Vector2 offset;

    [Header("Dash skill")]
    public float dashRange = 5f;
    public float dashSpeed = 50f;
    public float DashCD = 1f;

    [Header("Magic skill")]
    [SerializeField] private float magicSkillMana = 20;
    [SerializeField] private float magicDamage = 10f;
    [SerializeField] private float forceMagic = 10f;
    public float MagicCD = 1f;

    private float curHealthPoint = 0f;
    private float curManaPoint = 0f;

    [Header("Gameobject")]
    [SerializeField] private GameObject magicFire = null;
    [SerializeField] private Transform firePoint = null;
    private PlayerVFX vFXController;

    public void SaveData(ref GameData data)
    {
        data.playerScore = score;
    }
    public void LoadData(GameData data)
    {
        score = data.playerScore;
    }
    int a;
    private static PlayerCharacter instance;
    public static PlayerCharacter Instance
    {
        get
        {
            return instance;
        }
    }



    void Start()
    {
        instance = this;
        LoadData(DataPersistanceManager.Instance.getData());

        curHealthPoint = healthPoint;
        curManaPoint = ManaPoint;
        vFXController = GetComponent<PlayerVFX>();
        //vFXController.stopTrailVFX();
        UI_GameHUD.Instance.setMaxHealth(healthPoint);
        UI_GameHUD.Instance.setMaxMana(ManaPoint);
    }


    private void Update()
    {
        UI_GameHUD.Instance.setHealthValue(curHealthPoint);
        UI_GameHUD.Instance.setEnergyValue(curManaPoint);
        UI_GameHUD.Instance.setScore(this.score);
    }

    public void addScore(int score)
    {
        this.score += score;

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireCube(((Vector2)transform.position + offset * transform.localScale), size);


    //}

    void attackAnimationEvent()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + offset * transform.localScale, size, 0, Vector2.right, 0, LayerMask.GetMask("Monsters"));
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IMonsters>().takeDamage(attackDamage);
            vFXController.playExplosion(firePoint.transform.position);
            healMP(10);
        }
    }



    public bool hasMana()
    {
        return (curManaPoint > magicSkillMana);
    }



    public void setActiveTrail(bool value)
    {
        if (value)
            vFXController.playTrailVFX();
        else
            vFXController.stopTrailVFX();

    }

    public void playMagicSkill()
    {

    }

    public void playAttackSkill()
    {

        curManaPoint = curManaPoint + 5 > ManaPoint ? ManaPoint : curManaPoint + 5;

    }

    public void takeDamage(float damage)
    {

        this.curHealthPoint -= damage;
        StartCoroutine(flickerPlayer());
    }

    IEnumerator flickerPlayer()
    {
        Color color = GetComponent<SpriteRenderer>().color;

        for (int i = 0; i < 6; i++)
        {
            color.a = 0.5f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.05f);
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.05f);


        }


    }
    public void healHP(float HP)
    {
        curHealthPoint += HP;
    }
    public void healMP(float MP)
    {
        curManaPoint += MP;
    }

    public bool isDead()
    {
        return curHealthPoint <= 0;
    }

}
