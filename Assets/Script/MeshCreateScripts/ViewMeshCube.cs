using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMeshCube : MonoBehaviour
{
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        foreach(int i in mesh.triangles)
        {
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
