using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CreateGun : MonoBehaviour
{	
    public Material NewMat;
    Mesh mesh;
	Mesh GunMesh;

	[Header("First Mesh 24 vertices")]
	[SerializeField] Vector3[] vertices;
	[SerializeField] Vector2[] UVmapping;
	[SerializeField] int[] triangles;

    public GameObject player;
    public Rigidbody bullet;
    public Transform spawnPoint;
    private bool canShot = true;
    private bool isDead = false;
    public AudioSource shootSound;
    void Start () 
    {
		transform.rotation = transform.parent.transform.rotation;
		GameObject Object1 = new GameObject("Base", typeof(MeshFilter), typeof(MeshRenderer));
        Object1.transform.SetParent(transform);
        Object1.transform.localScale = new Vector3(1, 0.5f, 1);
		//Object1.transform.localRotation = transform.parent.transform.rotation;
		Object1.transform.position = transform.position;
        CreateBase(Object1);

		// GameObject Object2 = new GameObject("Gun", typeof(MeshFilter), typeof(MeshRenderer));
        // Object2.transform.SetParent(transform);
        // Object2.transform.localScale = new Vector3(1, 0.5f, 1);
		// Object1.transform.localRotation = transform.rotation;
		// Object2.transform.position = transform.position;
        // Gun(Object2);

        player = transform.parent.GetComponent<EnemyScript>().player;
	}

	private void CreateBase(GameObject Object) 
    {
		///////////////// Traditional way with 24 vertices //////////////////
		vertices = new Vector3[24]
		{
			//Front
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (0, 1, 0),

			//Back
			new Vector3 (-1, 0, 1),
			new Vector3 (2, 0, 1),
			new Vector3 (2, 1, 1),
			new Vector3 (-1, 1, 1),

			//Left
            new Vector3 (0, 0, 0),
            new Vector3 (-1, 0, 1),
            new Vector3 (-1, 1, 1),
            new Vector3 (0, 1, 0),

			//Right
            new Vector3 (1, 0, 0),
            new Vector3 (2, 0, 1),
			new Vector3 (2, 1, 1),
			new Vector3 (1, 1, 0),

			//Top
			new Vector3 (0, 1, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (2, 1, 1),
			new Vector3 (-1, 1, 1),

			//Bottom
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (2, 0, 1),
			new Vector3 (-1, 0, 1)
		};

		UVmapping = new Vector2[vertices.Length];
		for(int i = 0; i < UVmapping.Length;) 
		{
			if(vertices[i].x == vertices[i+1].x && vertices[i].x == vertices[i+2].x) 
			{
				UVmapping[i] = new Vector2(vertices[i].y, vertices[i].z);
				UVmapping[i+1] = new Vector2(vertices[i+1].y, vertices[i+1].z);
				UVmapping[i+2] = new Vector2(vertices[i+2].y, vertices[i+2].z);
				UVmapping[i+3] = new Vector2(vertices[i+3].y, vertices[i+3].z);
			}
			else
			{
				if(vertices[i].y == vertices[i+1].y && vertices[i].y == vertices[i+2].y) 
				{
					UVmapping[i] = new Vector2(vertices[i].x, vertices[i].z);
					UVmapping[i+1] = new Vector2(vertices[i+1].x, vertices[i+1].z);
					UVmapping[i+2] = new Vector2(vertices[i+2].x, vertices[i+2].z);
					UVmapping[i+3] = new Vector2(vertices[i+3].x, vertices[i+3].z);
				}
				else
				{
                    if(vertices[i].z == vertices[i+1].z && vertices[i].z == vertices[i+2].z)
                    {
                        UVmapping[i] = new Vector2(vertices[i].x, vertices[i].y);
                        UVmapping[i+1] = new Vector2(vertices[i+1].x, vertices[i+1].y);
                        UVmapping[i+2] = new Vector2(vertices[i+2].x, vertices[i+2].y);
                        UVmapping[i+3] = new Vector2(vertices[i+3].x, vertices[i+3].y);
                    }
                    else
                    {
                        UVmapping[i] = new Vector2(vertices[i].y, vertices[i].z);
                        UVmapping[i+1] = new Vector2(vertices[i+1].y, vertices[i+1].z);
                        UVmapping[i+2] = new Vector2(vertices[i+2].y, vertices[i+2].z);
                        UVmapping[i+3] = new Vector2(vertices[i+3].y, vertices[i+3].z);
                    }
                        
				}                
			} 
			i += 4;
    	}

		triangles = new int[]
		{
			0, 2, 1, //face front
			0, 3, 2,
			4, 5, 6, //face back
			4, 6, 7,
			8, 9, 10, //face left
			8, 10, 11,
			12, 14, 13, //face right
			12, 15, 14,			
			16, 18, 17, //face top
			16, 19, 18,
			20, 21, 22, //face bottom
			20, 22, 23
		};
        mesh = new Mesh();	
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = UVmapping;
		mesh.Optimize();
		mesh.RecalculateNormals();
        Object.GetComponent<MeshFilter>().mesh = mesh;
        Object.GetComponent<MeshRenderer>().material = NewMat;
	}

	// private void Gun(GameObject Object) 
    // {
	// 	///////////////// Traditional way with 24 vertices //////////////////
	// 	Vector3[] vertices = new Vector3[24]
	// 	{
	// 		//Front
	// 		new Vector3 (0.25f, 0.25f, -1),
	// 		new Vector3 (0.75f, 0.25f, -1),
	// 		new Vector3 (0.75f, 0.75f, -1),
	// 		new Vector3 (0.25f, 0.75f, -1),

	// 		//Back
	// 		new Vector3 (0.25f, 0.25f, 0),
	// 		new Vector3 (0.75f, 0.25f, 0),
	// 		new Vector3 (0.75f, 0.75f, 0),
	// 		new Vector3 (0.25f, 0.75f, 0),

	// 		//Left
    //         new Vector3 (0.25f, 0.25f, -1),
    //         new Vector3 (0.25f, 0.25f, 0),
    //         new Vector3 (0.25f, 0.75f, 0),
    //         new Vector3 (0.25f, 0.75f, -1),

	// 		//Right
    //         new Vector3 (0.75f, 0.25f, -1),
    //         new Vector3 (0.75f, 0.25f, 0),
	// 		new Vector3 (0.75f, 0.75f, 0),
	// 		new Vector3 (0.75f, 0.75f, -1),

	// 		//Top
	// 		new Vector3 (0.25f, 0.75f, -1),
	// 		new Vector3 (0.75f, 0.75f, -1),
	// 		new Vector3 (0.75f, 0.75f, 0),
	// 		new Vector3 (0.25f, 0.75f, 0),

	// 		//Bottom
	// 		new Vector3 (0.25f, 0.25f, -1),
	// 		new Vector3 (0.75f, 0.25f, -1),
	// 		new Vector3 (0.75f, 0.25f, 0),
	// 		new Vector3 (0.25f, 0.25f, 0)
	// 	};

	// 	Vector2[] UVmapping = new Vector2[vertices.Length];
	// 	for(int i = 0; i < UVmapping.Length;) 
	// 	{
	// 		if(vertices[i].x == vertices[i+1].x && vertices[i].x == vertices[i+2].x) 
	// 		{
	// 			UVmapping[i] = new Vector2(vertices[i].y, vertices[i].z);
	// 			UVmapping[i+1] = new Vector2(vertices[i+1].y, vertices[i+1].z);
	// 			UVmapping[i+2] = new Vector2(vertices[i+2].y, vertices[i+2].z);
	// 			UVmapping[i+3] = new Vector2(vertices[i+3].y, vertices[i+3].z);
	// 		}
	// 		else
	// 		{
	// 			if(vertices[i].y == vertices[i+1].y && vertices[i].y == vertices[i+2].y) 
	// 			{
	// 				UVmapping[i] = new Vector2(vertices[i].x, vertices[i].z);
	// 				UVmapping[i+1] = new Vector2(vertices[i+1].x, vertices[i+1].z);
	// 				UVmapping[i+2] = new Vector2(vertices[i+2].x, vertices[i+2].z);
	// 				UVmapping[i+3] = new Vector2(vertices[i+3].x, vertices[i+3].z);
	// 			}
	// 			else
	// 			{
    //                 if(vertices[i].z == vertices[i+1].z && vertices[i].z == vertices[i+2].z)
    //                 {
    //                     UVmapping[i] = new Vector2(vertices[i].x, vertices[i].y);
    //                     UVmapping[i+1] = new Vector2(vertices[i+1].x, vertices[i+1].y);
    //                     UVmapping[i+2] = new Vector2(vertices[i+2].x, vertices[i+2].y);
    //                     UVmapping[i+3] = new Vector2(vertices[i+3].x, vertices[i+3].y);
    //                 }
    //                 else
    //                 {
    //                     UVmapping[i] = new Vector2(vertices[i].y, vertices[i].z);
    //                     UVmapping[i+1] = new Vector2(vertices[i+1].y, vertices[i+1].z);
    //                     UVmapping[i+2] = new Vector2(vertices[i+2].y, vertices[i+2].z);
    //                     UVmapping[i+3] = new Vector2(vertices[i+3].y, vertices[i+3].z);
    //                 }
                        
	// 			}                
	// 		} 
	// 		i += 4;
    // 	}

	// 	int[] triangles = new int[]
	// 	{
	// 		0, 2, 1, //face front
	// 		0, 3, 2,
	// 		4, 5, 6, //face back
	// 		4, 6, 7,
	// 		8, 9, 10, //face left
	// 		8, 10, 11,
	// 		12, 14, 13, //face right
	// 		12, 15, 14,			
	// 		16, 18, 17, //face top
	// 		16, 19, 18,
	// 		20, 21, 22, //face bottom
	// 		20, 22, 23
	// 	};
    //     GunMesh = new Mesh();	
	// 	GunMesh.Clear();
	// 	GunMesh.vertices = vertices;
	// 	GunMesh.triangles = triangles;
	// 	GunMesh.uv = UVmapping;
	// 	GunMesh.Optimize();
	// 	GunMesh.RecalculateNormals();
    //     Object.GetComponent<MeshFilter>().mesh = GunMesh;
    //     Object.GetComponent<MeshRenderer>().material = NewMat;
	// }

    void Update()
    {
        GameObject Gun = gameObject;
		spawnPoint = Gun.transform;
		Vector3 range = (Gun.transform.position - player.transform.position);
        Vector3 track = (player.transform.position - Gun.transform.position).normalized;
		// track.x = 0;
		// track.z = 0;
		if(track.y < 0) track.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(track);
        Vector3 Rotate = Quaternion.Lerp(Gun.transform.rotation, lookRotation, Time.deltaTime * 2).eulerAngles;
        if(player != null && range.magnitude <= 15 && range.magnitude >= 0 && !isDead) 
        {
			//transform.LookAt(player.transform);
			Gun.transform.rotation = Quaternion.Euler(Rotate.x, Rotate.y, Rotate.z);
			//spawnPoint = gameObject.transform;
			if(player.activeSelf && canShot && gameObject.activeSelf)
			{
				StartCoroutine(Shoot(range.magnitude));
			}         
        }
    }

    IEnumerator Shoot(float range)
    {
        Rigidbody bulletInstance;            
        bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
        bulletInstance.AddForce(spawnPoint.forward * 3000f);
        shootSound.Play();    
        canShot = false;
        yield return new WaitForSeconds(0.5f);
        canShot = true;        
    }
}
