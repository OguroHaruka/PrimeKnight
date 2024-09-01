using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    bool playerDamage = PlayerScript.playerIsDamage;

    int Damage = PlayerScript.Power;
    public static bool isDamage;
    public Slider HP;
    public GameObject player;
    public Rigidbody rb;

    public MeshCollider meshColider;

    public Animator animator;

    float enemySpeed;

    bool isWait;
    bool isAttack;

    public int attackCount;

    public int count;

    public static bool isDead;

    bool hitPlayer;
    bool hitPlayer2;
    bool hitPlayer3;

    bool attackFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        attackCount = 0;
        count = 0;
        HP.value = 300;
        isDamage = false;
        isWait = false;
        attackFlag = false;
        isAttack = false;
        hitPlayer = false;
        hitPlayer2 = false;
        hitPlayer3 = false;
        isDead=false;
        enemySpeed = 0.008f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            if (isDamage == false)
            {
                HP.value -= Damage;
            }
            
        }

        if (attackCount == 0 || attackCount == 1 || attackCount == 2|| attackCount == 3)
        {
            if (other.gameObject.tag == "Player")
            {
                isAttack = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        meshColider.enabled = false;

        rb.velocity = new Vector3(0, 0, 0);
        
        if (attackCount==0)
        {
            AttackPattern1();
            if (HP.value <= 0)
            {
                animator.SetBool("Dead", true);
                attackCount = 4;
            }

        }
        else if (attackCount==1)
        {
            RemovePattern();
            if (HP.value <= 0)
            {
                animator.SetBool("Dead", true);
                attackCount = 4;
            }
        }
        else if(attackCount==2)
        {
            AttackPattern2();
            if (HP.value <= 0)
            {
                animator.SetBool("Dead", true);
                attackCount = 4;
            }
        }
        else if (attackCount == 3)
        {
            RemovePattern2();
            if (HP.value <= 0)
            {
                animator.SetBool("Dead", true);
                attackCount = 4;
            }
        }
        else if (attackCount == 4)
        {
            isDead = true;
            enemySpeed = 0.0f;
        }
    }

    private void AttackPattern1()
    {
        if (isAttack == false)
        {
            animator.SetBool("Walk", true);
        }

        if (isAttack == true)
        {
            animator.SetBool("Attack", true);
            enemySpeed = 0.0f;
        }

        var speed = Vector3.zero;
        speed.z = enemySpeed;
        transform.LookAt(player.transform);
        this.transform.Translate(speed);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            
                meshColider.enabled = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            

            isWait = true;
        }

        if (isWait == true)
        {
            count++;
        }

        if (count >= 300)
        {  
            count = 0;
            hitPlayer = false;
            playerDamage = false;
            attackCount = 1;
            animator.SetBool("Walk", true);
        }
    }

    private void AttackPattern2()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            meshColider.enabled = true;
        }
        

        var speed = Vector3.zero;
        speed.z = enemySpeed;
        transform.LookAt(player.transform);
        this.transform.Translate(speed);

        if (isAttack == false)
        {
            enemySpeed = 0.013f;
            animator.SetBool("Walk", true);
        }

        if (isAttack == true)
        {
            if (attackFlag == false)
            {
                animator.SetBool("Attack", true);
                enemySpeed = 0.0f;
                attackFlag = true;
            }
            count++;

            if (count >= 1000)
            {
                count = 0;
                attackCount = 3;
                playerDamage = false;
                animator.SetBool("Walk", true);
            }

        }



    }

    private void RemovePattern()
    {
        enemySpeed = 0.005f;
        
        var speed = Vector3.zero;
        speed.z = enemySpeed;
        transform.LookAt(player.transform);
        this.transform.Translate(-speed);

        isWait = false;
        isAttack = false;
        attackFlag = false;
        count++;

        if (count >= 300)
        {
            attackCount = 2;
            playerDamage = false;
            count = 0;
        }
        
    }

    private void RemovePattern2()
    {
        enemySpeed = 0.005f;

        var speed = Vector3.zero;
        speed.z = enemySpeed;
        transform.LookAt(player.transform);
        this.transform.Translate(-speed);

        isWait = false;
        isAttack = false;
        attackFlag = false;
        count++;



        if (count >= 1000)
        {
            playerDamage = false;
            attackCount = 0;
            count = 0;
        }

    }
}
