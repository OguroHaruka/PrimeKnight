using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    bool playerDamage = PlayerScript.playerIsDamage;
    int Damage = PlayerScript.Power;
    public Animator animator;
    public GameObject player;
    public Slider HP;
    public Rigidbody rb;
    public BoxCollider boxColider;

    public static bool isDamage;

    float speed = 0.03f;

    float setRot;
    float rotSpeed = 3;
    int removeRand;
    int actionRand;
    float animatorCount = 0;

    int actionPattern = 0;
    float removeCount = 0;

    bool playerCheck = false;
    bool removeRandFlag = false;

    bool isWait;
    bool isWalk;
    bool isBack;
    bool isDown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        switch (actionPattern)
        {
            case 0:
                Chase();
                break;
            case 1:
                Remove();
                break;
            case 2:
                Attack_A();
                break;
            case 3:
                Attack_B();
                break;
            case 4:
                Attack_C();
                break;
            case 5:

            break;
        }
    }

    void Chase()
    {
        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;

        DirSet();

        WalkAnime(true);

        speed = 0.04f;
        this.transform.Translate(Vector3.forward * speed);

        if (playerCheck == true)
        {
            actionRand = Random.Range(2, 5);
            actionPattern = actionRand;

            WalkAnime(false);

            if (actionPattern == 2)
            {
                animator.SetBool("Attack_A_1", true);
                animator.SetBool("Attack_A_2", true);
            }
            else if (actionPattern == 3)
            {
                animator.SetBool("Attack_B_1", true);
                animator.SetBool("Attack_B_2", true);
                animator.SetBool("Attack_B_3", true);
            }
            else if (actionPattern == 4)
            {
                animator.SetBool("Attack_C_1", true);
                animator.SetBool("Attack_C_2", true);
                animator.SetBool("Attack_C_3", true);
                animator.SetBool("Attack_C_4", true);
            }

        }

    }

    void Remove()
    {
        animatorCount = 0;
        rotSpeed = 3;

        removeCount += Time.deltaTime;

        if (removeRandFlag == false)
        {
            removeRandFlag = true;
            removeRand = Random.Range(1, 4);
        }

        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;

        DirSet();
        BackAnime(true);

        speed = 0.03f;
        this.transform.Translate(Vector3.forward * -speed);

        if (removeCount >= removeRand)
        {
            actionPattern = 0;

            removeCount = 0;
            removeRandFlag = false;
            BackAnime(false);
            playerCheck = false;
            animator.SetBool("Attack_A_1", false);
            animator.SetBool("Attack_A_2", false);

            animator.SetBool("Attack_B_1", false);
            animator.SetBool("Attack_B_2", false);
            animator.SetBool("Attack_B_3", false);

            animator.SetBool("Attack_C_1", false);
            animator.SetBool("Attack_C_2", false);
            animator.SetBool("Attack_C_3", false);
            animator.SetBool("Attack_C_4", false);
        }

    }

    void Attack_A()
    {
        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        DirSet();
        animatorCount += Time.deltaTime;
        if (animatorCount >= 0.4 && animatorCount <= 0.75)
        {
            speed = 0.04f;
            this.transform.Translate(Vector3.forward * speed);
        }
        if ((animatorCount >= 1.3))
        {
            actionPattern = 1;
        }
    }

    void Attack_B()
    {
        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        DirSet();
        rotSpeed = 10;
        animatorCount += Time.deltaTime;
        if (animatorCount >= 0.7 && animatorCount <= 0.9)
        {
            speed = 0.07f;
            this.transform.Translate(Vector3.forward * speed);
        }
        if ((animatorCount >= 1.5))
        {
            actionPattern = 1;
        }
    }

    void Attack_C()
    {
        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        DirSet();
        animatorCount += Time.deltaTime;

        if (animatorCount <= 0.35)
        {
            rotSpeed = 20;
            speed = 0.05f;
            this.transform.Translate(Vector3.forward * speed);
        }
        else
        {
            rotSpeed = 0;
        }

        if (animatorCount >= 1&& animatorCount <= 1.3)
        {
            speed = 0.04f;
            this.transform.Translate(Vector3.forward * speed);
        }

        if ((animatorCount >= 2.05))
        {
            actionPattern = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCheck = true;
        }

        if (other.gameObject.tag == "PlayerAttack")
        {
            if (isDamage == false)
            {
                HP.value -= Damage;
            }

        }

    }

    void WeaponColliderOn()
    {
        
    }

    void WeaponColliderOf()
    {

    }

    void WalkAnime(bool _isWalk)
    {
        if(isWalk != _isWalk)
        {
            animator.SetBool("Walk", _isWalk);
            isWalk = _isWalk;
        }
    }
    void BackAnime(bool _isBack)
    {
        if (isBack != _isBack)
        {
            animator.SetBool("Back", _isBack);
            isBack = _isBack;
        }
    }

    void WaitAnime(bool _isWait)
    {
        if (isWait != _isWait)
        {
            animator.SetBool("Wait", _isWait);
            isWait = _isWait;
        }
    }

    void DownAnime(bool _isDown)
    {
        if (isDown != _isDown)
        {
            animator.SetBool("Down", _isDown);
            isDown = _isDown;
        }
    }

    void DirSet()
    {
        var _angle = Mathf.LerpAngle(transform.eulerAngles.y, setRot, rotSpeed * Time.deltaTime);
        var _rot = Vector3.zero;
        _rot.y = _angle;
        transform.eulerAngles = _rot;
    }

}