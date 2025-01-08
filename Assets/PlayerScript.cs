using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject Recovery3;
    public GameObject Recovery2;
    public GameObject Recovery1;
    public GameObject Recovery0;

    public MeshCollider meshColider;
    bool isWeaponColider;
    public static bool playerIsDamage;
    public Slider hp;
    public Slider stamina;
    bool isStaminaReduce;
    bool isRollingStaminaReduce;
    bool isStaminaRecovery;
    bool isPlayerActioin;
    bool isRolling;
    float rollingTime=0;
    bool isRollingNow;
    bool isRecovery;
    public static bool isDead = false;
    int recoveryItem = 3;

    public ParticleSystem slashParticle;
    float slashEffectCount;
    bool slashEffectFlag1;
    bool slashEffectFlag2;

    public Animator animator;
    public Rigidbody rb;
    public new GameObject camera;

    public float moveSpeed;
    public Vector3 movingVelocity;

    Transform CamPos;
    private Vector3 Camforward;
    private Vector3 ido;
    private Vector3 Animdir = Vector3.zero;

    Vector3 latestPos;
    Vector3 movingDirection;

    public static int Power = 15;

    public static bool isBoDamage = true;
    public static bool isDamage = true;
    bool isDamageactive = true;

    // Start is called before the first frame update
    void Start()
    {
        playerIsDamage = false;
        CamPos = camera.transform;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 0.15f;
        stamina.value = 1000;
        hp.value = 100;
        isStaminaReduce = false;
        isRollingStaminaReduce = false;
        isStaminaRecovery = true;
        isPlayerActioin = true;
        isRolling = false;
        isRollingNow = false;
        isRecovery = false;
        isWeaponColider = true;
        isDead = false;
        recoveryItem = 3;
        Application.targetFrameRate = 60;
        slashEffectCount=0;
        slashEffectFlag1 = false;
        slashEffectFlag2 = false;
        isDamage = true;
        isDamageactive = true;
        isBoDamage = true;
        Recovery3.SetActive(false);
        Recovery2.SetActive(false);
        Recovery1.SetActive(false);
        Recovery0.SetActive(false);
        Power = 15;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRollingNow == false)
        {
            if (other.gameObject.tag == "EnemyAttack")
            {
                if (isBoDamage == true)
                {
                    hp.value -= 20;
                    isBoDamage = false;
                }
            }
        }
    }
        // Update is called once per frame
        void Update()
    {

        if (recoveryItem == 3)
        {
            Recovery3.SetActive(true);
            Recovery2.SetActive(false);
            Recovery1.SetActive(false);
            Recovery0.SetActive(false);
        }
        else if(recoveryItem == 2)
        {
            Recovery3.SetActive(false);
            Recovery2.SetActive(true);
            Recovery1.SetActive(false);
            Recovery0.SetActive(false);
        }
        else if (recoveryItem == 1)
        {
            Recovery3.SetActive(false);
            Recovery2.SetActive(false);
            Recovery1.SetActive(true);
            Recovery0.SetActive(false);
        }
        else if (recoveryItem == 0)
        {
            Recovery3.SetActive(false);
            Recovery2.SetActive(false);
            Recovery1.SetActive(false);
            Recovery0.SetActive(true);
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movingDirection = new Vector3(x, 0, z);
        movingDirection.Normalize();
        movingVelocity = movingDirection * moveSpeed;

        if (slashParticle.gameObject.activeSelf == true)
        {
            slashEffectCount+= Time.deltaTime;
        }

        if (slashEffectCount >= 0.4)
        {
            slashParticle.gameObject.SetActive(false);
            slashEffectCount = 0;
        }

        if (z > 0)
            {
                animator.SetBool("Walk", true);
            }
            else if (z < 0)
            {
                animator.SetBool("Walk", true);
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, 0.0f);
            }

            if (x < 0)
            {
                animator.SetBool("Walk", true);
            }
            else if (x > 0)
            {
                animator.SetBool("Walk", true);
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                rb.velocity = new Vector3(0.0f, rb.velocity.y, movingVelocity.z);
            }
        
        if (x == 0 && z == 0)
        {
            animator.SetBool("Walk", false);
        }
        if (isPlayerActioin == true)
        {
            if (stamina.value >= 150)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("Attack", true);
                }
            }
            if (stamina.value >= 300)
            {
                if (Input.GetMouseButtonDown(1) && (x != 0 || z != 0))
                {
                    animator.SetBool("Rolling", true);

                }
            }
        }

        if (isDead == true)
        {
            meshColider.enabled = false;
            moveSpeed = 0.0f;
            {
                animator.SetBool("Dead", true);
            }
        }

        if (isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && recoveryItem > 0)
            {
                animator.SetBool("Recovery", true);
            }

            if (isStaminaRecovery == true && isRollingStaminaReduce == false)
            {
                stamina.value += 4;
            }
            
            //if (stamina.value <= 0)
            //{
            //    isPlayerActioin = false;
            //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
            //    {
            //        moveSpeed = 0.03f;
            //    }
            //}

            if (stamina.value > 500)
            {
                isPlayerActioin = true;
                if (isStaminaRecovery == true)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
                    {
                        moveSpeed = 0.15f;
                    }
                }
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
            {
                if (isRolling == false)
                {
                    rollingTime = Time.deltaTime;
                    if (rollingTime <= 45)
                    {
                        isRollingNow = true;
                    }
                    isRolling = true;
                    if (isRollingStaminaReduce == false)
                    {
                        isRollingStaminaReduce = true;
                        stamina.value -= 300;
                    }
                }
            }
            else
            {
                isRolling = false;
                isRollingStaminaReduce = false;
                isRollingNow = false;
                rollingTime = 0;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
            {
                if (isRecovery == false)
                {
                    isRecovery = true;
                    recoveryItem -= 1;
                    hp.value += 60;
                }
                moveSpeed = 0.0f;
                
            }
            else
            {
                isRecovery = false;
            }
        }

        if (hp.value <= 0)
        {
            isDead = true;
        }

        if (isDead == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                
                if (isWeaponColider == true)
                {
                    meshColider.enabled = true;
                }

                moveSpeed = 0.0f;
                isStaminaRecovery = false;
                if (isStaminaReduce == false)
                {
                    isStaminaReduce = true;
                    stamina.value -= 150;
                }
                if (slashEffectFlag1 == false)
                {
                    slashParticle.gameObject.SetActive(true);
                    slashEffectFlag1 = true;
                }

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                if (isDamageactive == true)
                {
                    isDamage = true;
                    isDamageactive = false;
                }
                if (isWeaponColider == false)
                {
                    meshColider.enabled = true;
                }

                Power = 20;
                if (isStaminaReduce == true)
                {
                    isStaminaReduce = false;
                    stamina.value -= 200;
                }

                if (slashEffectFlag2 == false)
                {
                    slashParticle.gameObject.SetActive(true);
                    slashEffectFlag2 = true;
                }
            }
            else
            {
                if (isPlayerActioin == true)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
                    {
                        moveSpeed = 0.15f;
                    }
                }
                Power = 15;
                meshColider.enabled = false;
                isStaminaReduce = false;
                isStaminaRecovery = true;
                isWeaponColider = true;
                slashEffectFlag1 = false;
                slashEffectFlag2 = false;
                isDamage = true;
                isDamageactive = true;
            }
        }

    }

    public void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Camforward = Vector3.Scale(CamPos.forward, new Vector3(1, 0, 1)).normalized;
        ido = v * Camforward * moveSpeed + h * CamPos.right * moveSpeed;
        transform.position = new Vector3(
        transform.position.x + ido.x,
        0,
        transform.position.z + ido.z);
        Vector3 AnimDir = ido;
        AnimDir.y = 0;
        if (AnimDir.sqrMagnitude > 0.001)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, AnimDir, 5f * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (movingDirection == new Vector3(0, 0, 0)) return;
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }
        rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
    }

}
