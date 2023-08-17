using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slower : MonoBehaviour
{
    public Sequence mySequence;
    public float dmg = 10f;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //DOTween.SetTweensCapacity(100000, 50);
        if(gameObject.tag == "BigSlower")
        {
            //Debug.Log("mySequence");
            mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMoveY(transform.position.y - 1f, 0.5f).SetEase(Ease.OutSine)).SetLoops(-1, LoopType.Yoyo).SetDelay(1f).SetLink(gameObject);           
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(transform == null)
        // {
        //     mySequence.Kill();
        //     DOTween.Kill(transform);
        // }
    }
}
