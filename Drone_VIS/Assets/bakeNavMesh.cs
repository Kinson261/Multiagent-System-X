using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



/*THIS SCRIPT IS USED FOR BAKING NAVMESH DYNAMICALLY AFTER ADDING BUILDINGS TO THE MAP*/


public class bakeNavMesh : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    void Update()
    {
        for(int i =0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
