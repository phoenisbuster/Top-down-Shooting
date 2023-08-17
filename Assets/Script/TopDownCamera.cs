using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    
    public GameObject target;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() 
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
        //transform.LookAt(target.transform.position);
    }
}
