using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainCreator : MonoBehaviour
{
    [SerializeField]
    int segments = 100;
    [SerializeField]
    float outerRadius = 1f;
    [SerializeField]
    float innerRadius = 0.5f;


    [ContextMenu("drawMesh")]
    void MakeMesh()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] vertices = new Vector3[segments * 2];
        int[] triangles = new int[segments * 6];

        float angleStep = 360.0f / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            vertices[i * 2] = new Vector3(Mathf.Cos(angle) * outerRadius, 0, Mathf.Sin(angle) * outerRadius);
            vertices[i * 2 + 1] = new Vector3(Mathf.Cos(angle) * innerRadius, 0, Mathf.Sin(angle) * innerRadius);

            triangles[i * 6] = i * 2;
            triangles[i * 6 + 1] = (i * 2 + 3) % (segments * 2);
            triangles[i * 6 + 2] = i * 2 + 1;
            triangles[i * 6 + 3] = i * 2;
            triangles[i * 6 + 4] = (i * 2 + 2) % (segments * 2);
            triangles[i * 6 + 5] = (i * 2 + 3) % (segments * 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
