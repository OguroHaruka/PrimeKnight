using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    bool isEnemyDamage = EnemyScript.isDamage;

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
    bool isRecovery;
    public static bool isDead = false;
    int recoveryItem = 3;

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

    public static int Power = 10;

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
        isRecovery = false;
        isWeaponColider = true;
        isDead = false;
        recoveryItem = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {

                hp.value -= 2;


        }
    }
        // Update is called once per frame
        void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movingDirection = new Vector3(x, 0, z);
        movingDirection.Normalize();
        movingVelocity = movingDirection * moveSpeed;

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
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Attack", true);
            }

            if (Input.GetMouseButtonDown(1) && (x != 0 || z != 0))
            {
                animator.SetBool("Rolling", true);
                
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
                stamina.value += 1;
            }

            if (stamina.value <= 0)
            {
                isPlayerActioin = false;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
                {
                    moveSpeed = 0.03f;
                }
            }

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
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Recovery"))
            {
                if (isRecovery == false)
                {
                    isRecovery = true;
                    recoveryItem -= 1;
                }
                moveSpeed = 0.0f;
                hp.value += 40;
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
                if (isEnemyDamage == true)
                {
                    isWeaponColider = false;
                    meshColider.enabled = false;
                }

                moveSpeed = 0.0f;
                isStaminaRecovery = false;
                if (isStaminaReduce == false)
                {
                    isStaminaReduce = true;
                    stamina.value -= 150;
                }

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                if (isWeaponColider == false)
                {
                    isEnemyDamage = false;
                    meshColider.enabled = true;
                }
                if (isEnemyDamage == true)
                {

                    isWeaponColider = true;
                    meshColider.enabled = false;
                }

                Power = 15;
                if (isStaminaReduce == true)
                {
                    isStaminaReduce = false;
                    stamina.value -= 200;
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
                Power = 10;
                meshColider.enabled = false;
                isStaminaReduce = false;
                isStaminaRecovery = true;
                isEnemyDamage = false;
                isWeaponColider = true;
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
