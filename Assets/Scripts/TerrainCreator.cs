using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainCreator : MonoBehaviour
{
    [SerializeField] float outerRadius = 5f;
    [SerializeField] float innerRadius = 2f;
    [SerializeField, Range(3f, 100f)] int numberOfSegments = 100;
    [SerializeField, Range(0f, 360f)] float arcDegrees = 360f;

    private MeshFilter meshFilter;

    void Start() 
    {
        GenerateCircleWithHole();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            GenerateCircleWithHole();
        }
    }

    void GenerateCircleWithHole()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Calculate the number of segments based on the arc
        int actualSegments = Mathf.CeilToInt((numberOfSegments * arcDegrees) / 360f);
        float deltaTheta = Mathf.Deg2Rad * arcDegrees / actualSegments;
        float theta = 0f;

        // Vertices
        Vector3[] vertices = new Vector3[(actualSegments + 1) * 2]; // Increase vertices count by 1

        for (int i = 0; i <= actualSegments; i++) // Change loop condition
        {
            float xOuter = Mathf.Cos(theta) * outerRadius;
            float yOuter = Mathf.Sin(theta) * outerRadius;

            float xInner = Mathf.Cos(theta) * innerRadius;
            float yInner = Mathf.Sin(theta) * innerRadius;

            vertices[i] = new Vector3(xOuter, yOuter, 0f);
            vertices[i + actualSegments + 1] = new Vector3(xInner, yInner, 0f); // Increase index by 1

            theta += deltaTheta;
        }

        // Triangles
        int[] triangles = new int[actualSegments * 6];
        for (int i = 0, ti = 0; i < actualSegments; i++, ti += 6)
        {
            int next = (i + 1) % (actualSegments + 1); // Change loop condition

            triangles[ti] = i;
            triangles[ti + 1] = i + actualSegments + 1;
            triangles[ti + 2] = next + actualSegments + 1;

            triangles[ti + 3] = i;
            triangles[ti + 4] = next + actualSegments + 1;
            triangles[ti + 5] = next;
        }

        // Assigning data to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}