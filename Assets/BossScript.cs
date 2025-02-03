using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    bool playerDamage = PlayerScript.playerIsDamage;
    public static bool bossAttack_A;
    bool bossAttack_B;
    bool bossAttack_C_1;
    bool bossAttack_C_2;
    int Damage = PlayerScript.Power;
    public Animator animator;
    public GameObject player;
    public Slider HP;
    public Rigidbody rb;
    public BoxCollider boxColider;
    public ParticleSystem slashParticle;
    public ParticleSystem deathParticle;
    public ParticleSystem damageParticle;

    private AudioSource attackSound;

    public static bool isDamage = true;

    bool attackDamageActive = true;

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
    public static bool isDown;
    // Start is called before the first frame update
    void Start()
    {
        attackSound = GetComponent<AudioSource>();
        attackSound.Stop();
        player = GameObject.Find("Player");
        HP.value = 300;
        Application.targetFrameRate = 60;
        slashParticle.gameObject.SetActive(false);
        deathParticle.gameObject.SetActive(false);
        isDamage = true;
        attackDamageActive = true;
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
        if (HP.value <= 0)
        {
            DownAnime(true);
            actionPattern = 5;
            animator.SetBool("Attack_A_1", false);
            animator.SetBool("Attack_A_2", false);

            animator.SetBool("Attack_B_1", false);
            animator.SetBool("Attack_B_2", false);
            animator.SetBool("Attack_B_3", false);

            animator.SetBool("Attack_C_1", false);
            animator.SetBool("Attack_C_2", false);
            animator.SetBool("Attack_C_3", false);
            animator.SetBool("Attack_C_4", false);

            deathParticle.gameObject.SetActive(true);

            WaitAnime(false);
            WalkAnime(false);
            BackAnime(false);
            WeaponColliderOf();
            damageParticle.gameObject.SetActive(false);
        }
    }

    void Chase()
    {
        //ƒvƒŒƒCƒ„[‚Ì•ûŒü‚ÉŒü‚­
        var _dir = player.transform.position - transform.position;
        setRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        DirSet();

        WalkAnime(true);

        PlayerScript.isBoDamage=true;
        attackDamageActive = true;

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

        slashParticle.gameObject.SetActive(false);

        PlayerScript.isBoDamage = true;
        attackDamageActive = true;

        bossAttack_A =false;
        bossAttack_B=false;
        bossAttack_C_1=false;
        bossAttack_C_2=false;

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
        bossAttack_A = true;
        if (animatorCount >= 0.4 && animatorCount <= 0.75)
        {
            speed = 0.04f;
            this.transform.Translate(Vector3.forward * speed);
        }
        if (animatorCount >= 0.4 && animatorCount <= 0.9)
        {
            slashParticle.gameObject.SetActive(true);
        }
        else
        {
            slashParticle.gameObject.SetActive(false);
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
        bossAttack_B = true;
        if (animatorCount >= 0.7 && animatorCount <= 0.9)
        {
            speed = 0.07f;
            this.transform.Translate(Vector3.forward * speed);
            
        }
        if (animatorCount >= 0.7 && animatorCount <= 1.0)
        {
            slashParticle.gameObject.SetActive(true);
        }
        else
        {
            slashParticle.gameObject.SetActive(false);
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
            bossAttack_C_1 = true;
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
            if (attackDamageActive == true)
            {
                PlayerScript.isBoDamage = true;
                attackDamageActive = false;
            }
            bossAttack_C_2 = true;
            speed = 0.04f;
            this.transform.Translate(Vector3.forward * speed);
        }

        if (animatorCount >= 0.35 && animatorCount <= 1.5)
        {
            slashParticle.gameObject.SetActive(true);
        }
        else
        {
            slashParticle.gameObject.SetActive(false);
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
            if (PlayerScript.isDamage == true)
            {
                HP.value -= Damage;
                PlayerScript.isDamage = false;
                damageParticle.gameObject.SetActive(true);
                attackSound.Play();
            }
            
        }
        else
        {
            damageParticle.gameObject.SetActive(false);
        }

    }

    void WeaponColliderOn()
    {
        boxColider.enabled = true;
    }

    void WeaponColliderOf()
    {
        boxColider.enabled = false;
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

    public bool GetAttack_A()
    {
        return bossAttack_A;
    }
    public bool GetAttack_B()
    {
        return bossAttack_B;
    }
    public bool GetAttack_C_1()
    {
        return bossAttack_C_1;
    }
    public bool GetAttack_C_2()
    {
        return bossAttack_C_2;
    }
}
