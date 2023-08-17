using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public float speed = 2;
    
    //public Vector2 direction2;
    //public Vector3 direction3;
    private Rigidbody rb;
    private float disToScreen;
    public bool isClick = false;
    Vector3 originPos;
    public float Hp = 1000f;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveByTransform();
        MoveByMouseClick();
        //OnDestroyPlayer();
    }

    // void FixedUpdate() 
    // {
    //     //MoveByPhysics();
    //     //MoveByForce();
    // }

    // public bool isGrounded = true;
    // void OnCollisionEnter(Collision theCollision)
    // {
    //     if(theCollision.gameObject.tag == "Floor")
    //     {
    //         isGrounded = true;
    //     }        
    // }
 
    // //consider when character is jumping .. it will exit collision.
    // void OnCollisionExit(Collision theCollision)
    // {
    //     if (theCollision.gameObject.tag == "Floor")
    //     {
    //         isGrounded = false;
    //     }
    // }

    void OnTriggerEnter(Collider theCollision) 
    {
        if(theCollision.gameObject.tag == "Bullet")
        {
            Hp -= 2;
        }
    }

    void OnDestroyPlayer()
    {
        if(Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
    // void MoveByTransform()
    // {
    //     if(isGrounded)
    //     {
    //         float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    //         transform.Translate(horizontal, 0, 0);
    //         transform.Rotate(0, 30*horizontal, 0);         
    //         float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    //         transform.Translate(0, 0, vertical);
    //         if(Input.GetKeyDown(KeyCode.Space))
    //         {
    //             rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    //         }
    //     }
    // }

    //public Vector3 worldPosition;
    public bool CanClick = true;
    void MoveByMouseClick()
    {
        Ray ray;
        if(Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;            
            Debug.DrawRay(ray.origin, ray.direction * 10);
            if(Physics.Raycast(ray, out hitData))
            {
                //transform.DOMove(new Vector3(hitPosition.x, 1.5f, hitPosition.z), 2f);
                StopAllCoroutines();
                StartCoroutine(Delay(hitData));
                Debug.Log(transform.position + " C");
            }
            
        }       
    }

    IEnumerator Delay(RaycastHit target)
    {
        // Vector3 direction = target.point - transform.position;
        // direction.y = 1.5f;
        int i = 0;
        Debug.Log(target.point + " A");
        //Debug.Log(transform.position + " B");
        while(Mathf.Abs(target.point.x - transform.position.x) > 0.01f || Mathf.Abs(target.point.z - transform.position.z) > 0.01f)
        {
            if(target.collider.tag == "Floor")
            {
                Debug.Log(++i);
                controller.SimpleMove((target.point - transform.position) * speed * Time.deltaTime);
                Debug.Log(transform.position + " B");
            }
            else
            {
                break;
            }
            if(i > 300)
            {
                break;
            }
            //transform.LookAt(direction);
            yield return new WaitForSeconds(0.02f);
        }
        
    }

    // Vector3 screenPoint;
    // Vector3 offset;
    // void OnMouseDown()
    // {
    //     screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //     offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    // }

    // void OnMouseDrag()
    // {
    //     Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
    //     Vector3 newPos = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        
    //     Vector3 temp = originPos - newPos;
    //     temp.y = 0;
    //     if(newPos.y < 1.5) newPos.y = 1.5f;
    //     //if(newPos.x < 1.5) newPos.x = 1.5f;
    //     //if(newPos.z < 1.5) newPos.z = 1.5f;
    //     // if(temp.sqrMagnitude < 250)
    //     // {
    //     //     transform.position = new Vector3(newPos.x, 1.5f, newPos.z); 
    //     // }
    //     transform.position = new Vector3(newPos.x, newPos.y, gameObject.transform.position.z); 
    // }

    // void MoveByPhysics()
    // {
    //     if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         rb.velocity = Vector3.left * 100 * speed * Time.deltaTime;
    //     }
    //     if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //     {
    //         rb.velocity = Vector3.right * 100 * speed * Time.deltaTime;
    //     }
    //     if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    //     {
    //         rb.velocity = Vector3.forward * 100 * speed * Time.deltaTime;
    //     }
    //     if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    //     {
    //         rb.velocity = Vector3.forward * -100 * speed * Time.deltaTime;
    //     }
    //     // if(Input.GetKeyDown(KeyCode.Space))
    //     // {
    //     //     rb.velocity = Vector3.up * 100 * speed * Time.deltaTime;
    //     // }
    // }

    // void MoveByForce()
    // {
    //     if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         rb.AddForce(Vector3.left * 10 * speed * Time.deltaTime, ForceMode.Impulse);
    //     }
    //     if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //     {
    //         rb.AddForce(Vector3.right * 10 * speed * Time.deltaTime, ForceMode.Impulse);
    //     }
    //     if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    //     {
    //         rb.AddForce(Vector3.forward * 10 * speed * Time.deltaTime, ForceMode.Impulse);
    //     }
    //     if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    //     {
    //         rb.AddForce(Vector3.forward * -10 * speed * Time.deltaTime, ForceMode.Impulse);
    //     }
    // }
    
}
