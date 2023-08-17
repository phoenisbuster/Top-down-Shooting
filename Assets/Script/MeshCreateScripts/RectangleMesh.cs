using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class RectangleMesh : MonoBehaviour
{
    Mesh mesh;
    public Material NewMat;
    [SerializeField] List<Vector3> newVertices;
    [SerializeField] List<Vector2> newUV;
    [SerializeField] List<int> newTriangles;
    
    // Start is called before the first frame update
    void Start()
    {
        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();

        newVertices.Add(new Vector3(0,1));
        newVertices.Add(new Vector3(3,1));
        newVertices.Add(new Vector3(0,0));
        newVertices.Add(new Vector3(3,0));
        // new Vector3(0,1),
        // new Vector3(3,1),
        // new Vector3(0,0),
        // new Vector3(3,0)
        
        newUV.Add(new Vector2(0,1));
        newUV.Add(new Vector2(1,1));
        newUV.Add(new Vector2(0,0));
        newUV.Add(new Vector2(1,0));
        // new Vector2(0,1),
        // new Vector2(1,1),
        // new Vector2(0,0),
        // new Vector2(1,0)

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(2);
        newTriangles.Add(2);
        newTriangles.Add(1);
        newTriangles.Add(3);
        // 2,3,1,
        // 0,2,1

        mesh = new Mesh();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();

        GameObject Object = new GameObject("Rect", typeof(MeshFilter), typeof(MeshRenderer));
        Object.transform.SetParent(transform);
        Object.transform.localScale = new Vector3(30, 30, 1);
        Object.transform.position = transform.position;

        Object.GetComponent<MeshFilter>().mesh = mesh;
        Object.GetComponent<MeshRenderer>().material = NewMat;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(newVertices.Count() == 4)
		{
			mesh.Clear();
            for(int i = 0; i < 4; i++)
            {
                newVertices.Add(newVertices[i]);
                newUV.Add(newUV[i]);
            }
			newTriangles.Add(2);
            newTriangles.Add(3);
            newTriangles.Add(1);
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(1);
            mesh.vertices = newVertices.ToArray();
            mesh.uv = newUV.ToArray();
            mesh.triangles = newTriangles.ToArray();
			mesh.Optimize();
		}
		else
		{
			mesh.Clear();
            while(newVertices.Count() > 4 || newTriangles.Count() > 6)
            {
                if(newVertices.Count() > 4)
                {
                    newVertices.RemoveAt(newVertices.Count()-1);
                    newUV.RemoveAt(newUV.Count()-1);
                }
                if(newTriangles.Count() > 6)
                    newTriangles.RemoveAt(newTriangles.Count()-1);
            }
			mesh.vertices = newVertices.ToArray();
            mesh.uv = newUV.ToArray();
            mesh.triangles = newTriangles.ToArray();
			mesh.Optimize();
		}
    }

    void OnApplicationQuit() 
	{
		Destroy(transform.GetChild(0));
	}
}
