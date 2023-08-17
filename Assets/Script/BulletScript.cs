using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float dmg = 5f;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, startPos) > 30f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider theCollision)
    {        
        if(theCollision.gameObject.tag == "Player" || theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Terrain" || theCollision.gameObject.tag == "Enemy")
        {            
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}
