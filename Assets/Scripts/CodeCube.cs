using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeCube : MonoBehaviour
{
    private Mesh customMesh;

    // Start is called before the first frame update
    void Start()
    {
        var mesh = new Mesh();

        var verts = new Vector3[24];

        // front
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(0, 1, 0);
        verts[2] = new Vector3(1, 0, 0);
        verts[3] = new Vector3(1, 1, 0);

        // back
        verts[4] = new Vector3(0, 0, 1);
        verts[5] = new Vector3(0, 1, 1);
        verts[6] = new Vector3(1, 0, 1);
        verts[7] = new Vector3(1, 1, 1);

        // left
        verts[8] = new Vector3(1, 0, 0);
        verts[9] = new Vector3(1, 1, 0);
        verts[10] = new Vector3(1, 0, 1);
        verts[11] = new Vector3(1, 1, 1);

        // right
        verts[12] = new Vector3(0, 0, 0);
        verts[13] = new Vector3(0, 1, 0);
        verts[14] = new Vector3(0, 0, 1);
        verts[15] = new Vector3(0, 1, 1);

        // top
        verts[16] = new Vector3(0, 1, 0);
        verts[17] = new Vector3(0, 1, 1);
        verts[18] = new Vector3(1, 1, 0);
        verts[19] = new Vector3(1, 1, 1);

        // bottom
        verts[20] = new Vector3(0, 0, 1);
        verts[21] = new Vector3(0, 0, 0);
        verts[22] = new Vector3(1, 0, 1);
        verts[23] = new Vector3(1, 0, 0);

        mesh.vertices = verts;

        var indices = new int[36];

        // front
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 3;

        indices[3] = 3;
        indices[4] = 2;
        indices[5] = 0;

        // back
        indices[6] = 7;
        indices[7] = 5;
        indices[8] = 6;

        indices[9] = 5;
        indices[10] = 4;
        indices[11] = 6;

        // left
        indices[12] = 8;
        indices[13] = 9;
        indices[14] = 11;

        indices[15] = 11;
        indices[16] = 10;
        indices[17] = 8;

        // right
        indices[18] = 14;
        indices[19] = 15;
        indices[20] = 13;

        indices[21] = 13;
        indices[22] = 12;
        indices[23] = 14;

        // top
        indices[24] = 16;
        indices[25] = 17;
        indices[26] = 19;

        indices[27] = 19;
        indices[28] = 18;
        indices[29] = 16;

        // bottom
        indices[30] = 20;
        indices[31] = 21;
        indices[32] = 23;

        indices[33] = 23;
        indices[34] = 22;
        indices[35] = 20;

        mesh.triangles = indices;

        var norms = new Vector3[24];

        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;

        norms[4] = Vector3.forward;
        norms[5] = Vector3.forward;
        norms[6] = Vector3.forward;
        norms[7] = Vector3.forward;

        norms[8] = Vector3.right;
        norms[9] = Vector3.right;
        norms[10] = Vector3.right;
        norms[11] = Vector3.right;

        norms[12] = -Vector3.right;
        norms[13] = -Vector3.right;
        norms[14] = -Vector3.right;
        norms[15] = -Vector3.right;

        norms[16] = Vector3.up;
        norms[17] = Vector3.up;
        norms[18] = Vector3.up;
        norms[19] = Vector3.up;

        norms[20] = -Vector3.up;
        norms[21] = -Vector3.up;
        norms[22] = -Vector3.up;
        norms[23] = -Vector3.up;

        mesh.normals = norms;

        var UVs = new Vector2[24];

        // front
        UVs[0] = new Vector3(0, 0, 0);
        UVs[1] = new Vector3(0, 1, 0);
        UVs[2] = new Vector3(1, 0, 0);
        UVs[3] = new Vector3(1, 1, 0);

        // back
        UVs[4] = new Vector3(0, 0, 0);
        UVs[5] = new Vector3(0, 1, 0);
        UVs[6] = new Vector3(1, 0, 0);
        UVs[7] = new Vector3(1, 1, 0);

        // left
        UVs[8] = new Vector3(0, 0, 0);
        UVs[9] =  new Vector3(0, 1, 0);
        UVs[10] = new Vector3(1, 0, 0);
        UVs[11] = new Vector3(1, 1, 0);

        // right
        UVs[12] = new Vector3(0, 0, 0);
        UVs[13] = new Vector3(0, 1, 0);
        UVs[14] = new Vector3(1, 0, 0);
        UVs[15] = new Vector3(1, 1, 0);

        // top
        UVs[16] = new Vector3(0, 0, 0);
        UVs[17] = new Vector3(0, 1, 0);
        UVs[18] = new Vector3(1, 0, 0);
        UVs[19] = new Vector3(1, 1, 0);

        // bottom
        UVs[20] = new Vector3(0, 0, 0);
        UVs[21] = new Vector3(0, 1, 0);
        UVs[22] = new Vector3(1, 0, 0);
        UVs[23] = new Vector3(1, 1, 0);

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