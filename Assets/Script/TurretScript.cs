using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody bullet;
    public Rigidbody enemy;
    public Transform spawnPoint;
    private bool canShot = true;
    private bool isDead = false;
    public float MaxHp = 250f;
    public float Hp;
    public HealthBar healthBarHub;
    private float i = 0;
    public AudioSource shootSound;
    public AudioSource spawnSound;
    public AudioSource DestroySound;
    void Start()
    {
        Hp = MaxHp;
        healthBarHub.SetMaxHealth(MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        OnDestroyTurret();
        Vector3 range = (transform.position - player.transform.position);
        Vector3 track = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(track);
        Vector3 Rotate = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2).eulerAngles;
        if(player != null && range.magnitude <= 25 && range.magnitude >= 0 && !isDead) 
        {
            //transform.LookAt(player);
            transform.rotation = Quaternion.Euler(Rotate.x, Rotate.y, Rotate.z);
            //spawnPoint = gameObject.transform;
            if(player.activeSelf && canShot && gameObject.activeSelf)
            {
                StartCoroutine(Shoot(range.magnitude));
            }
            
        }
    }

    IEnumerator Shoot(float range)
    {
        if(range >= 3 && range <= 15)
        {
            Rigidbody bulletInstance;            
            bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
            bulletInstance.AddForce(spawnPoint.forward * 3000f);
            shootSound.Play();
        }        
        if(i == 0 && range <= 25)
        {
            Rigidbody enemySpawn;
            spawnSound.Play();
            enemySpawn = Instantiate(enemy, new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation) as Rigidbody;
            if(range <= 15)
                i = 40;
            else
                i = 20;
        }
        canShot = false;
        yield return new WaitForSeconds(0.1f);
        if(i > 0)
            i -= 1f;
        canShot = true;        
    }

    public void TakeDamage(float dmgDeal)
    {
        if(Hp - dmgDeal > 0)
        {
            Hp -= dmgDeal;
            healthBarHub.SetHealth(Hp);
            player.GetComponent<PlayerController>().dmgDeal += dmgDeal;
        }
        else
        {
            player.GetComponent<PlayerController>().dmgDeal += Hp;
            Hp -= dmgDeal;
            healthBarHub.SetHealth(Hp);
            OnDestroyTurret();
        }        
    }

    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage(theCollision.gameObject.GetComponent<PlayerBullet>().dmg);
        }
        if(theCollision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(theCollision.gameObject.GetComponent<EnemyBullet>().dmg);
        }
    }

    public void OnDestroyTurret()
    {
        if(Hp <= 0 && !isDead)
        {
            DestroySound.Play();
            isDead = true;
            player.GetComponent<PlayerController>().Score += 25;
            //Destroy(gameObject);
            StopAllCoroutines();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy() 
    {        
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
