using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent enemy;
    public float lookRadius = 20f;

    public float MaxHp = 100f;
    public float Hp;
    public float AttackDmg = 25f;
    public float AtkSpeed = 1f;
    public float CurAtkSpeed = 0.0f;
    public float ShootSpeed = 0.0f;
    public float speed = 4f;
    public float SlowSpeed = 3f;
    public HealthBar healthBarHub;    
    private bool canAtk = true;
    public bool isDead = false;
    private bool isKnockBack = false;

    public Animator anim;
    public Rigidbody[] DropItem;

    public AudioSource chaseSound1;
    public AudioSource chaseSound2;
    public AudioSource AtkSound;
    public AudioSource HitSound;
    public AudioSource DeadSound;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GetComponent<NavMeshAgent>();
        Hp = MaxHp;
        healthBarHub.SetMaxHealth(MaxHp);
        anim = GetComponentInChildren<Animator>();
        chaseSound2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        OnDestroyEnemy();
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= lookRadius && !isDead && !isKnockBack)
        {
            enemy.SetDestination(player.transform.position);
            
            if(!player.activeSelf || Time.timeScale == 0)
            {
                //Debug.Log("Stop Sound");
                chaseSound2.Pause();
            }
            else
            {
                chaseSound2.UnPause();
            }
                
            if(distance >= 30)
            {
                enemy.speed = 10;
            }
            else
            {
                enemy.speed = 4;
                //chaseSound2.Play();
            }
            if(distance <= enemy.stoppingDistance || (distance <= enemy.stoppingDistance + 1 && player.transform.position.y > 1.5f))
            {
                Debug.Log(distance);
                //Start to Atk
                //Face the player
                FacePlayer();
                if(canAtk && CurAtkSpeed <= 0)
                {
                    enemy.SetDestination(player.transform.position);
                    if(player.activeSelf || Time.timeScale != 0) AtkSound.Play();
                    AttackPlayer();
                }                    
            }
        }
    }

    void FixedUpdate()
    {
        if(CurAtkSpeed > 0)
        {
            CurAtkSpeed -= 0.02f;
            anim.SetBool("Atk", false);
        }
        else
            anim.SetBool("Atk", true);
    }

    void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackPlayer()
    {
        CurAtkSpeed = AtkSpeed;
        anim.SetTrigger("Attack");
        player.GetComponent<PlayerController>().TakeDamage(AttackDmg);
    }

    //Draw the area that enemy will detect player
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void OnEnable() 
    {
        GameManagerScript.changePhrase += GunSpawn;
    }

    void OnDisable()
    {
        GameManagerScript.changePhrase -= GunSpawn;
    }
    void GunSpawn()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void TakeDamage(float dmgDeal)
    {        
        if(Hp - dmgDeal > 0)
        {
            Hp -= dmgDeal;
            healthBarHub.SetHealth(Hp);
            player.GetComponent<PlayerController>().dmgDeal += dmgDeal; 
            //StopAllCoroutines();
            //StartCoroutine(KnockBack());
        }
        else
        {
            player.GetComponent<PlayerController>().dmgDeal += Hp;
            Hp -= dmgDeal;
            healthBarHub.SetHealth(Hp);
            OnDestroyEnemy();
        }        
    }

    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "PlayerBullet")
        {            
            StopCoroutine(KnockBack(theCollision.gameObject.transform.position, theCollision.gameObject.GetComponent<PlayerBullet>().dmg/3));
            StartCoroutine(KnockBack(theCollision.gameObject.transform.position, theCollision.gameObject.GetComponent<PlayerBullet>().dmg/3));
            TakeDamage(theCollision.gameObject.GetComponent<PlayerBullet>().dmg);
        }
        if(theCollision.gameObject.tag == "Bullet")
        {
            StopCoroutine(KnockBack(theCollision.gameObject.transform.position, theCollision.gameObject.GetComponent<BulletScript>().dmg));
            StartCoroutine(KnockBack(theCollision.gameObject.transform.position, theCollision.gameObject.GetComponent<BulletScript>().dmg));
            TakeDamage(theCollision.gameObject.GetComponent<BulletScript>().dmg);
        }
        if(theCollision.gameObject.tag == "BigSlower")
        {
            Hp -= 10;
            healthBarHub.SetHealth(Hp);
        }
        if(theCollision.gameObject.tag == "DeathBorder")
        {
            Hp = 0;
            healthBarHub.SetHealth(Hp);
        }
    }

    void OnTriggerStay(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower" && !isDead)
        {
            enemy.speed = SlowSpeed;
        }
    }

    void OnTriggerExit(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "BigSlower" && !isDead)
        {
            enemy.speed = speed;
        }
    }

    public IEnumerator KnockBack(Vector3 collision, float KnockBackPower)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        isKnockBack = true;
        canAtk = false;
        Vector3 direction = (collision - transform.position).normalized;
        direction.y = 0;
        rb.AddForce(direction * KnockBackPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.25f);
        rb.isKinematic = true;
        isKnockBack = false;
        canAtk = true;
    }
    
    public void DropSystem()
    {  
        float i = UnityEngine.Random.Range(0, 100);
        int j;
        Debug.Log(i);
        if(0 < i  && i<= 25f)
            j = 0;
        else if(i > 25f && i <= 40f)
            j = 1;
        else if(i > 40f && i <= 55f)
            j = 2;
        else if(i > 55f && i <= 70f)
            j = 3;
        else if(i > 70f && i <= 85f)
            j = 4;
        else
            j = 5;
        Rigidbody itemDrop;
        itemDrop = Instantiate(DropItem[j], transform.position, Quaternion.identity) as Rigidbody;
    }
    public void OnDestroyEnemy()
    {
        if(Hp <= 0 && !isDead)
        {            
            //DeadSound.Play();
            isDead = true;
            player.GetComponent<PlayerController>().Score += 5;
            enemy.enabled = false;
            //Destroy(gameObject);
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
        DropSystem();
        Destroy(gameObject);
    }
}
