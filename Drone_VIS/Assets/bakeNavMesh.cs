using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bakeNavMesh : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
