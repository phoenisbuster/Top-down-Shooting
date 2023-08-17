using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(Vector3.Distance(transform.position, startPos) > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider theCollision)
    {
        if(theCollision.gameObject.tag == "Turret" || theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Terrain" || theCollision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
