using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float dmg = 15f;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, startPos) > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider theCollision)
    {
        if(theCollision.gameObject.tag == "Turret" || theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Terrain" || theCollision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
