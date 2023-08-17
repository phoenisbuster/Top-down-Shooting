using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ItemsPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //DOTween.SetTweensCapacity(100000, 50);
        if(transform != null)
        {
            transform.DORotate(new Vector3(360, 0, 0), 1f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);
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

    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "Player")
        {
            //gameObject.SetActive(false);
            DOTween.Kill(transform);
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "Player")
        {
            DOTween.Kill(transform);
            Destroy(gameObject);
        }
    }
}
