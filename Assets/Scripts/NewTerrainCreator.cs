using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class NewTerrainCreator : MonoBehaviour
{
    public float height = 5f;
    public float outerRadius = 2f;
    public float innerRadius = 1f;
    public int segments = 100;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateCylinderWithHole(height, outerRadius, innerRadius, segments);
    }

    Mesh CreateCylinderWithHole(float height, float outerRadius, float innerRadius, int segments)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(segments + 1) * 4];
        int[] triangles = new int[segments * 12];

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            vertices[i * 4] = new Vector3(x * outerRadius, 0, z * outerRadius);
            vertices[i * 4 + 1] = new Vector3(x * outerRadius, height, z * outerRadius);
            vertices[i * 4 + 2] = new Vector3(x * innerRadius, 0, z * innerRadius);
            vertices[i * 4 + 3] = new Vector3(x * innerRadius, height, z * innerRadius);

            if (i < segments)
            {
                int baseIndex = i * 12;
                int vertIndex = i * 4;
                triangles[baseIndex] = vertIndex;
                triangles[baseIndex + 1] = vertIndex + 4;
                triangles[baseIndex + 2] = vertIndex + 1;
                triangles[baseIndex + 3] = vertIndex + 1;
                triangles[baseIndex + 4] = vertIndex + 4;
                triangles[baseIndex + 5] = vertIndex + 5;
                triangles[baseIndex + 6] = vertIndex + 2;
                triangles[baseIndex + 7] = vertIndex + 6;
                triangles[baseIndex + 8] = vertIndex + 3;
                triangles[baseIndex + 9] = vertIndex + 3;
                triangles[baseIndex + 10] = vertIndex + 6;
                triangles[baseIndex + 11] = vertIndex + 7;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        Debug.Log("finish");

        return mesh;
    }
}
