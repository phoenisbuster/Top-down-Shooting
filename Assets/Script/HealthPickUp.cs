using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthPickUp : MonoBehaviour
{
    public float healValue = 100f;
    // Start is called before the first frame update
    void Start()
    {
         DOTween.SetTweensCapacity(100000, 50);
        if(transform != null && gameObject.tag == "Health")
        {
            transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform == null)
        {
            DOTween.Kill(transform);
        }
    }

    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "Player")
        {
            theCollision.gameObject.GetComponent<PlayerController>().HealPlayer(healValue);
            theCollision.gameObject.GetComponent<PlayerController>().HealthPickup.Play();
            if(gameObject.tag == "Health")
            {
                DOTween.Kill(transform);
            }
            Destroy(gameObject);
        }
    }
}
