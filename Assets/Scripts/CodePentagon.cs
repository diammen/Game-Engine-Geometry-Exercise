using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePentagon : MonoBehaviour
{
    private Mesh customMesh;

    // Start is called before the first frame update
    void Start()
    {
        var mesh = new Mesh();

        var verts = new Vector3[6];

        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(0, 1, 0);
        verts[2] = new Vector3(Mathf.Cos(18 * Mathf.Deg2Rad), Mathf.Sin(18 * Mathf.Deg2Rad), 0);
        verts[3] = new Vector3(Mathf.Cos(-54 * Mathf.Deg2Rad), Mathf.Sin(-54 * Mathf.Deg2Rad), 0);
        verts[4] = new Vector3(-Mathf.Cos(-54 * Mathf.Deg2Rad), Mathf.Sin(-54 * Mathf.Deg2Rad), 0);
        verts[5] = new Vector3(-Mathf.Cos(18 * Mathf.Deg2Rad), Mathf.Sin(18 * Mathf.Deg2Rad), 0);
        mesh.vertices = verts;

        var indices = new int[15];

        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;

        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;

        indices[6] = 0;
        indices[7] = 3;
        indices[8] = 4;

        indices[9] = 0;
        indices[10] = 4;
        indices[11] = 5;

        indices[12] = 0;
        indices[13] = 5;
        indices[14] = 1;

        mesh.triangles = indices;

        var norms = new Vector3[6];

        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        norms[4] = -Vector3.forward;
        norms[5] = -Vector3.forward;

        mesh.normals = norms;

        var UVs = new Vector2[6];

        UVs[0] = new Vector3(0, 0, 0);
        UVs[1] = new Vector3(0, 1, 0);
        UVs[2] = new Vector3(Mathf.Cos(18 * Mathf.Deg2Rad), Mathf.Sin(18 * Mathf.Deg2Rad), 0);
        UVs[3] = new Vector3(Mathf.Cos(-54 * Mathf.Deg2Rad), Mathf.Sin(-54 * Mathf.Deg2Rad), 0);
        UVs[4] = new Vector3(-Mathf.Cos(-54 * Mathf.Deg2Rad), Mathf.Sin(-54 * Mathf.Deg2Rad), 0);
        UVs[5] = new Vector3(-Mathf.Cos(18 * Mathf.Deg2Rad), Mathf.Sin(18 * Mathf.Deg2Rad), 0);

        mesh.uv = UVs;

        var filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
        customMesh = mesh;
    }

    private void OnDestroy()
    {
        if (customMesh != null)
        {
            Destroy(customMesh);
        }
    }
}
