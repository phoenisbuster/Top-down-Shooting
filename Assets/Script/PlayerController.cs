using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /////////////* Vars for base stats*//////////////
    public float MaxHp = 1000f;
    public float Hp;
    public float MaxStamina = 100f;
    public float Stamina;
    public float MaxMana = 100f;
    public float Mana = 0;
    public float AttackDmg;
    public float NormalAtk = 50f;
    public float lifeStealth = 0.25f;
    public float TotalShield = 0;
    public float CurShield = 0;
    public float AtkSpeed = 1f;
    public float DefaultShootSpeed = 0.5f;
    public float ShootSpeed = 0.0f;
    ////////////////////////////////////////////////

    ///////////* Vars for char controller */////////
    private CharacterController controller;
    public bool isDead = false;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private bool checkGround;
    private bool isRunning = false;
    ///////////////////////////////////////////////

    ///////////* Var for char movement *///////////
    private float Speed;
    private float slowSpeed = 1.5f;
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f;
    private bool isRun = false;
    private bool isSlow = false;
    private bool isAtk = false;
    private bool canMove = true;
    ///////////////////////////////////////////////

    /////////////* Var for cursor *////////////////
    public Texture2D cursorTexture;
    public Texture2D cursorBase;
    public Texture2D cursorTurret;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool CanChangeCur = false;
    ///////////////////////////////////////////////

    /////////////* Var for Hub ////////////////////
    public HealthBar healthBarUI;
    public HealthBar healthBarHub;
    public HealthBar staminaBarUI;
    public GameObject Shield;
    public HealthBar ShieldUI;
    public int Score;
    public bool canAtk = true;
    public bool canShoot = true;
    ///////////////////////////////////////////////

    ///////////*Vars for buff syste/ */////////////
    private float IncreaseDamageTime = 0f;
    public GameObject dmgBuff;
    private float VampTime = 0f;
    private float VampValue = 0.5f;
    private float VampValuePerStack = 0.5f;
    public GameObject Vamp;
    private float InvincibleTime = 0.0f;
    public GameObject Invincible;
    private float StrengthTime = 0.0f;
    public GameObject Strength;
    ///////////////////////////////////////////////

    ///////////*Var for game over *////////////////
    public float dmgDeal = 0;
    public float totalHeal = 0;
    public int buffNo = 0;
    ///////////////////////////////////////////////

    public Rigidbody bullet;
    public Transform spawnPoint;
    public Camera cam;
    public Animator anim;

    ///////////////*Var for sound//////////////////
    public AudioSource shootSound;
    public AudioSource AtkSound;
    public AudioSource HitSound;
    public AudioSource DeadSound;
    public AudioSource FootStep;
    public AudioSource HealthPickup;
    public AudioSource ArmorPickup;
    public AudioSource StaminaPickup;
    public AudioSource PowerPickup;
    public AudioSource LifeStealthPickup;
    public AudioSource ImmortalPickup;

    ///////////////////////////////////////////////

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Confined;
        Hp = MaxHp;
        healthBarUI.SetMaxHealth(MaxHp);
        healthBarHub.SetMaxHealth(MaxHp);
        Stamina = MaxStamina;
        staminaBarUI.SetMaxHealth(MaxStamina);
        Mana = MaxMana;
        ShieldUI = Shield.GetComponent<HealthBar>();
        AttackDmg = NormalAtk;
        //anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(Time.timeScale == 0)
            FootStep.Pause();
        if(!isDead && gameObject.activeSelf)
        {
            OnDestroyPlayer();
            groundedPlayer = (controller.isGrounded || checkGround);
            Debug.Log(controller.isGrounded);
            if(groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                //anim.SetFloat("isGround", Convert.ToSingle(groundedPlayer) * jumpHeight * 5);
                //anim.SetFloat("isGround", 10);
            }
            else
            {
                FootStep.Pause();
            }
            if(Input.GetKey(KeyCode.LeftShift) && (Stamina >= 1 || StrengthTime > 0))
                isRun = true;
            else
                isRun = false;            
            //Speed = isRun? runSpeed : walkSpeed;
            //Vector3 move = groundedPlayer? new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) : Vector3.zero;
            Vector3 move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            if(gameObject.activeSelf && canMove)
            {
                controller.Move(move * Time.deltaTime * Speed); 
            }                
            if(move != Vector3.zero && canMove)
            {
                //gameObject.transform.forward = move;
                if(groundedPlayer) FootStep.UnPause();
                //anim.SetBool("IsMove", true);
                anim.SetFloat("Speed", Speed);
                if(isRun)
                {
                    isRunning = true;
                    //RunStamina();
                }
                else
                    isRunning = false;                                
            }
            else if(move == Vector3.zero && canMove)
            {
                anim.SetFloat("Speed", 0);
                FootStep.Pause();
            }
            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer && (Stamina >= 25 || StrengthTime > 0) && canMove)
            {
                FootStep.Pause();
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                JumpStamina();
                checkGround = false;
                anim.SetFloat("isGround", 0);
            }
            anim.SetFloat("isGround", Convert.ToSingle(groundedPlayer) * jumpHeight * 5);
            if(gameObject.activeSelf)
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // if(Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
            // {
            //     CreateRay(ray);
            // }
            if(CanChangeCur)
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 temp = hit.point;
                    temp.y = transform.position.y;
                    transform.LookAt(temp);
                    if(hit.collider.tag.Equals("Turret") || hit.collider.tag.Equals("Enemy"))
                    {
                        Cursor.SetCursor(cursorTurret, Vector2.zero, cursorMode);
                    }
                    else
                        Cursor.SetCursor(cursorBase, Vector2.zero, cursorMode);
                }               
            }

            if(Input.GetMouseButtonDown(1))
            {                
                RaycastHit AttackHit;
                if(Physics.Raycast(ray, out AttackHit) && canAtk && gameObject.activeSelf && (Stamina >= 15 || StrengthTime > 0) && Time.timeScale != 0)
                {
                    StopCoroutine(Attack());
                    anim.SetTrigger("Atk");
                    AtkStamina();
                    FootStep.Pause();
                    AtkSound.Play();
                    Vector3 range = transform.position - AttackHit.collider.transform.position;
                    range.y = 0;
                    //transform.LookAt(new Vector3(AttackHit.point.x, transform.position.y, AttackHit.point.z));                   
                    if(AttackHit.collider.tag == "Turret" && range.magnitude <= 6)
                    {
                        AttackHit.collider.GetComponent<TurretScript>().TakeDamage(AttackDmg);
                        if(VampTime > 0)
                        {
                            HealPlayer(AttackDmg*VampValue);
                        }
                    }
                    if(AttackHit.collider.tag == "Enemy" && range.magnitude <= 6)
                    {
                        AttackHit.collider.GetComponent<EnemyScript>().TakeDamage(AttackDmg);
                        AttackHit.collider.GetComponent<EnemyScript>().StartCoroutine(AttackHit.collider.GetComponent<EnemyScript>().KnockBack(-transform.position, 10));
                        HealPlayer(AttackDmg*lifeStealth);
                        if(VampTime > 0)
                        {
                            HealPlayer(AttackDmg*VampValue);
                        }
                    }
                    StartCoroutine(Attack());
                }                
            } 
            //Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) , Input.GetKey(KeyCode.Q)
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {                
                RaycastHit shoot;
                if(isAtk == false && Physics.Raycast(ray, out shoot) && gameObject.activeSelf && (Stamina >= 10 || StrengthTime > 0) && Time.timeScale != 0)
                {
                    StopCoroutine(Shoot());
                    ShotStamina();
                    shootSound.Play();
                    //ShootSpeed = DefaultShootSpeed;                    
                    anim.SetTrigger("Shoot");
                    //transform.LookAt(new Vector3(shoot.point.x, transform.position.y, shoot.point.z));
                    Rigidbody bulletInstance;
                    bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
                    bulletInstance.AddForce(spawnPoint.forward * 1500f);
                    StartCoroutine(Shoot());       
                }
                            
            }
            //anim.SetFloat("isShoot", ShootSpeed);       
        }            
    }

    void FixedUpdate() 
    {
        if(!isDead && gameObject.activeSelf)
        {
            StaminaRegain();
            if(isRunning)
                RunStamina();
            if(isSlow)
            {
                Speed = slowSpeed;
                if(isRun)
                {
                    Speed += Speed + 0.03f < runSpeed? 0.03f : runSpeed-Speed;
                }
                else
                {
                    Speed += Speed + 0.03f < walkSpeed? 0.03f : walkSpeed-Speed;
                } 
            }
            else
            {
                if(isRun)
                {
                    Speed += Speed + 0.03f < runSpeed? 0.03f : runSpeed-Speed;
                }
                else
                {
                    Speed -= Speed - 0.1f > walkSpeed? 0.1f : Speed-walkSpeed;
                } 
            }     

            if(IncreaseDamageTime > 0)
            {
                IncreaseDamageTime -= 0.02f;
                dmgBuff.GetComponent<Image>().fillAmount = IncreaseDamageTime/10f;
            }                
            else
            {
                dmgBuff.SetActive(false);
                AttackDmg = NormalAtk;
            } 

            if(VampTime > 0)
            {
                VampTime -= 0.02f;
                Vamp.GetComponent<Image>().fillAmount = VampTime/10f;
            }                
            else
            {
                Vamp.SetActive(false);
                VampValue = VampValuePerStack;
            } 

            if(CurShield > 0)
            {
                CurShield -= 0.5f;
                ShieldUI.SetHealth(CurShield);
            }                
            else
            {
                TotalShield = 0;
                CurShield = 0;
                Shield.SetActive(false);
            }

            if(InvincibleTime > 0)
            {
                InvincibleTime -= 0.02f;
                Invincible.GetComponent<Image>().fillAmount = InvincibleTime/5f;
            }                
            else
            {
                Invincible.SetActive(false);
            }

            if(StrengthTime > 0)
            {
                StrengthTime -= 0.02f;
                Strength.GetComponent<Image>().fillAmount = StrengthTime/5f;
            }                
            else
            {
                DefaultShootSpeed = 0.5f;
                Strength.SetActive(false);
            }               
        }
    }
    void CreateRay(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {   
            if (hit.collider.tag.Equals("Floor") && gameObject.activeSelf)
            {                
                //StopCoroutine(loop(hit));
                StopAllCoroutines();
                StartCoroutine(changeCursor());
                StartCoroutine(loop(hit));
                StartCoroutine(stopMove());
            }            
        }
    }
    IEnumerator loop(RaycastHit hit)
    {
        canAtk = false;
        canShoot = false;
        canHurt = false;
        while ((Mathf.Abs(hit.point.x - transform.position.x) > 0.01f) || (Mathf.Abs(hit.point.z - transform.position.z) > 0.01f))
        {            
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            controller.Move((hit.point - gameObject.transform.position).normalized * Speed * Time.deltaTime);  
            //anim.SetBool("IsMove", true);
            anim.SetFloat("Speed", Speed);
            FootStep.UnPause();
            if(isRun)
                //RunStamina();
                isRunning = true;
            else
                isRunning = false;
            yield return new WaitForSeconds(0.02f);
        }
        //anim.SetBool("IsMove", false);   
    }
    IEnumerator stopMove()
    {
        canAtk = true;
        canHurt = true;
        canShoot = true;
        yield return new WaitForSeconds(2);
        StopAllCoroutines();
    }

    IEnumerator changeCursor()
    {
        CanChangeCur = true;
        yield return new WaitForSeconds(0.05f);
        CanChangeCur = false;

    }

    IEnumerator Attack()
    {
        canAtk = false;
        canHurt = true;
        isAtk = true;
        canMove = false;        
        yield return new WaitForSeconds(AtkSpeed);
        anim.SetTrigger("FinishAtk");
        canMove = true;
        isAtk = false;
        canAtk = true;
    }

    IEnumerator Shoot()
    {
        canAtk = false;
        canHurt = true;
        isAtk = true;
        canShoot = false;
        CanChangeCur = true;
        yield return new WaitForSeconds(DefaultShootSpeed);
        anim.SetTrigger("FinishShoot");
        CanChangeCur = false;
        canShoot = true;
        isAtk = false;
        canAtk = true;
    }

    void RunStamina()
    {
        if(Time.timeScale != 0)
        {
            Stamina -= StrengthTime <= 0? 0.5f : 0;
            staminaBarUI.SetHealth(Stamina);
        }        
    }

    void JumpStamina()
    {
        if(Time.timeScale != 0)
        {
            Stamina -= StrengthTime <= 0? 25 : 0;
            staminaBarUI.SetHealth(Stamina);
        }
        
    }

    void AtkStamina()
    {
        if(Time.timeScale != 0)
        {
            Stamina -= StrengthTime <= 0? 15 : 0;
            staminaBarUI.SetHealth(Stamina);
        }
    }

    void ShotStamina()
    {
        if(Time.timeScale != 0)
        {
            Stamina -= StrengthTime <= 0? 10f : 0;
            staminaBarUI.SetHealth(Stamina);
        }
    }

    void StaminaRegain()
    {
        if(!isDead && groundedPlayer && canMove && !isRunning && StrengthTime <= 0)
            Stamina += Stamina + 0.25f <= 100? 0.25f : 100 - Stamina;            
        else if(!isDead && StrengthTime > 0)
            Stamina += Stamina + 0.5f <= 100? 0.5f : 100 - Stamina;
        staminaBarUI.SetHealth(Stamina);
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Floor")
        {
            checkGround = true;
            //anim.SetFloat("isGround", 10);
        }
    }

    void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Floor")
        {
            checkGround = true;
            //anim.SetFloat("isGround", 10);
        }
    }
    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Floor")
        {
            checkGround = false;
        }
    }
    private bool canHurt = true;
    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "DeathBorder")
        {
            InvincibleTime = 0;
            TakeDamage(MaxHp*100);
        }
        if(theCollision.gameObject.tag == "Bullet")
        {
            TakeDamage(theCollision.gameObject.GetComponent<BulletScript>().dmg);
        }
        if(theCollision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(theCollision.gameObject.GetComponent<EnemyBullet>().dmg);
        }
        if(theCollision.gameObject.tag == "BigSlower" && canHurt)
        {
            Debug.Log("Hurt");
            isSlow = true;
            StartCoroutine(HurtPlayer(theCollision.gameObject));
        }
        if(theCollision.gameObject.tag == "PowerUp")
        {
            Debug.Log("IncreaseDamage");
            PowerPickup.Play();
            dmgBuff.SetActive(true);
            staminaBarUI.SetHealth(Stamina);
            AttackDmg += NormalAtk * 0.5f;
            IncreaseDamageTime = 10f;
            buffNo++;
        }
        if(theCollision.gameObject.tag == "Vamp")
        {
            Debug.Log("LifeStealth");
            LifeStealthPickup.Play();
            Vamp.SetActive(true);
            VampValue += VampValuePerStack;
            VampTime = 10f;
            buffNo++;
        }
        if(theCollision.gameObject.tag == "Shield")
        {
            Debug.Log("ShieldOn");
            ArmorPickup.Play();
            Shield.SetActive(true);
            TotalShield += 250;
            CurShield += 250;
            ShieldUI.SetMaxHealth(TotalShield);
            ShieldUI.SetHealth(CurShield);
            buffNo++;
        }
        if(theCollision.gameObject.tag == "Immortality")
        {
            Debug.Log("Invincible");
            ImmortalPickup.Play();
            Invincible.SetActive(true);
            InvincibleTime = 5f;
            buffNo++;
        }
        if(theCollision.gameObject.tag == "Strength")
        {
            Debug.Log("More Stamina");
            StaminaPickup.Play();
            Strength.SetActive(true);
            StrengthTime = 5f;
            DefaultShootSpeed = 0.3f;
            buffNo++;
        }
    }

    void OnTriggerStay(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower")
        {
            isSlow = true;
        }
        if(theCollision.gameObject.tag == "DeathBorder")
        {
            TakeDamage(MaxHp*100);
        }
    }

    void OnTriggerExit(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower")
        {
            isSlow = false;
        }
    }

    IEnumerator HurtPlayer(GameObject Needles)
    {
        canHurt = false;
        TakeDamage(Needles.GetComponent<Slower>().dmg);
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }

    public void HealPlayer(float heal)
    {
        Hp += Hp + heal <= MaxHp? heal : MaxHp - Hp;
        totalHeal += Hp + heal <= MaxHp? heal : MaxHp - Hp;
        healthBarUI.SetHealth(Hp);
        healthBarHub.SetHealth(Hp);
    }

    public void TakeDamage(float dmgRecieved)
    {
        if(InvincibleTime > 0)
        {
            dmgRecieved = 0;
        }
        if(CurShield > 0)
        {
            CurShield -= CurShield - dmgRecieved >= 0? dmgRecieved : CurShield;
            ShieldUI.SetHealth(CurShield);
        }
        else
        {
            Hp -= Hp - dmgRecieved >= 0? dmgRecieved : Hp;
            healthBarUI.SetHealth(Hp);
            healthBarHub.SetHealth(Hp);
            if(Hp <= 0)
                OnDestroyPlayer();
        }     
    }

    void OnDestroyPlayer()
    {
        if(Hp <= 0 && !isDead)
        {
            isDead = true;
            //gameObject.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy() 
    {
        //DropSystem();
        DeadSound.Play();
        anim.SetTrigger("Death");
        //isDead = true;
        //player.GetComponent<PlayerController>().Score += 5;
        yield return new WaitForSeconds(0.75f);
        gameObject.SetActive(false);
    }
}
