using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    public float rotateSpeed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(100000, 200);
        if(transform != null && gameObject.tag == "SpecialMoney")
        {
            transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            transform.DOMoveY(transform.position.y + 1.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if(gameObject.tag == "Money")
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.Self);
        }
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
            if(gameObject.tag == "SpecialMoney")
            {
                DOTween.Kill(transform);
            }
            Destroy(gameObject);
        }

    }
}
