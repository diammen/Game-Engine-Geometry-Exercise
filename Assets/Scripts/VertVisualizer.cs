using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertVisualizer : MonoBehaviour
{
    private MeshFilter filter;
    public int highlight = -1;

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
    }

    private void OnDrawGizmos()
    {
        if (filter != null)
        {
            int highlightedIndex = Mathf.Clamp(highlight, -1, filter.mesh.vertexCount - 1);

            Gizmos.color = Color.yellow;

            foreach (Vector3 vert in filter.mesh.vertices)
            {
                Gizmos.DrawSphere(vert + transform.position, 0.1f);
            }

            if (highlightedIndex != -1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.TransformPoint(filter.mesh.vertices[highlightedIndex]), 0.2f);
            }
        }
    }
}