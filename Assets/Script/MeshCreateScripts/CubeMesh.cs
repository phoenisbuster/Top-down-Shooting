using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[ExecuteInEditMode]
[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CubeMesh : MonoBehaviour
{	
    public Material NewMat;
    Mesh mesh;

	[Header("First Mesh 24 vertices")]
	[SerializeField] Vector3[] vertices;
	[SerializeField] Vector2[] UVmapping;
	[SerializeField] int[] triangles;

	[Space(10)]

	[Header("Sec Mesh 14 vertices")]
	[SerializeField] Vector3[] Vertices;
	[SerializeField] Vector2[] UVs;
	[SerializeField] int[] Triangles;

    void Start () 
    {
		GameObject Object = new GameObject("Cube", typeof(MeshFilter), typeof(MeshRenderer));
        Object.transform.SetParent(transform);
        Object.transform.localScale = new Vector3(30, 30, 30);
		Object.transform.position = transform.position;
        CreateCube(Object);
	}

	private void CreateCube(GameObject Object) 
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
			new Vector3 (0, 0, 1),
			new Vector3 (1, 0, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (0, 1, 1),

			//Left
            new Vector3 (0, 0, 0),
            new Vector3 (0, 0, 1),
            new Vector3 (0, 1, 1),
            new Vector3 (0, 1, 0),

			//Right
            new Vector3 (1, 0, 0),
            new Vector3 (1, 0, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (1, 1, 0),

			//Top
			new Vector3 (0, 1, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (1, 1, 1),
			new Vector3 (0, 1, 1),

			//Bottom
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 0, 1),
			new Vector3 (0, 0, 1)
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
					UVmapping[i] = new Vector2(vertices[i].x, vertices[i].y);
					UVmapping[i+1] = new Vector2(vertices[i+1].x, vertices[i+1].y);
					UVmapping[i+2] = new Vector2(vertices[i+2].x, vertices[i+2].y);
					UVmapping[i+3] = new Vector2(vertices[i+3].x, vertices[i+3].y);
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

		////////////////////// New way with 14 vertices
		float size = 1f;
		Vertices = new Vector3[]
		{
			new Vector3(0, size, 0),
			new Vector3(0, 0, 0),
			new Vector3(size, size, 0),
			new Vector3(size, 0, 0),

			new Vector3(0, 0, size),
			new Vector3(size, 0, size),
			new Vector3(0, size, size),
			new Vector3(size, size, size),

			new Vector3(0, size, 0),
			new Vector3(size, size, 0),

			new Vector3(0, size, 0),
			new Vector3(0, size, size),

			new Vector3(size, size, 0),
			new Vector3(size, size, size),
		};

		UVs = new Vector2[]
		{
			new Vector2(0, 0.66f),
			new Vector2(0.25f, 0.66f),
			new Vector2(0, 0.33f),
			new Vector2(0.25f, 0.33f),

			new Vector2(0.5f, 0.66f),
			new Vector2(0.5f, 0.33f),
			new Vector2(0.75f, 0.66f),
			new Vector2(0.75f, 0.33f),

			new Vector2(1, 0.66f),
			new Vector2(1, 0.33f),

			new Vector2(0.25f, 1),
			new Vector2(0.5f, 1),

			new Vector2(0.25f, 0),
			new Vector2(0.5f, 0),
		};

		Triangles = new int[]
		{
			0, 2, 1, // front
			1, 2, 3,
			4, 5, 6, // back
			5, 7, 6,
			6, 7, 8, //top
			7, 9 ,8, 
			1, 3, 4, //bottom
			3, 5, 4,
			1, 11,10,// left
			1, 4, 11,
			3, 12, 5,//right
			5, 12, 13


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

	void Update()
    {
        // if(EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode && transform.childCount > 0)
		// {
		// 	Destroy(transform.GetChild(0).gameObject);
		// 	DestroyGameObject();
		// }
			
		if(Input.GetKeyDown(KeyCode.D))
            DoubleMatByUV(mesh.uv);
        if(Input.GetKeyDown(KeyCode.A))
            HalfMatByUV(mesh.uv);
		if(Input.GetKeyDown(KeyCode.W))
            IncreaseMatByUV(mesh.uv);
        if(Input.GetKeyDown(KeyCode.S))
            DecreaseMatByUV(mesh.uv);
		if(Input.GetKeyDown(KeyCode.Space))
			ChangeMesh(mesh);
    }

	public void DoubleMatByUV(Vector2[] newUV)
    {
        for(int i = 0; i < newUV.Length; i++)
		{
			newUV[i].x *= 2;
			newUV[i].y *= 2;
			//Debug.Log(newUV[i]);
		}
        mesh.uv = newUV;
    }
    public void HalfMatByUV(Vector2[] newUV)
    {
        for(int i = 0; i < newUV.Length; i++)
		{
			newUV[i].x /= 2;
			newUV[i].y /= 2;
			//Debug.Log(newUV[i]);
		}
        mesh.uv = newUV;
    }
	public void IncreaseMatByUV(Vector2[] newUV)
    {
        for(int i = 0; i < newUV.Length; i++)
		{
			newUV[i].x += 1;
			newUV[i].y += 1;
			//Debug.Log(newUV[i]);
		}
        mesh.uv = newUV;
    }

	public void DecreaseMatByUV(Vector2[] newUV)
    {
        for(int i = 0; i < newUV.Length; i++)
		{
			newUV[i].x -= 1;
			newUV[i].y -= 1;
			//Debug.Log(newUV[i]);
		}
        mesh.uv = newUV;
    }

	public void ChangeMesh(Mesh mesh)
	{
		if(mesh.vertices.Length == 24)
		{
			mesh.Clear();
			mesh.vertices = Vertices;
			mesh.triangles = Triangles;
			mesh.uv = UVs;
			mesh.Optimize();
			mesh.RecalculateNormals();
		}
		else
		{
			mesh.Clear();
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = UVmapping;
			mesh.Optimize();
			mesh.RecalculateNormals();
		}
	}

	void DestroyGameObject() 
	{
		//Debug.Log(transform.GetChild(0).gameObject);
	}
}

