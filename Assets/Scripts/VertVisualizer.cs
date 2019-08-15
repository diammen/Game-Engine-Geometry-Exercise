using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertVisualizer : MonoBehaviour
{
    private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (filter != null)
        {
            foreach (Vector3 vert in filter.mesh.vertices)
            {
                Gizmos.DrawSphere(vert + transform.position, 0.1f);
            }
        }
    }
}