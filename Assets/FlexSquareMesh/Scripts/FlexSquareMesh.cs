using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public sealed class FlexSquareMesh : MonoBehaviour
{
    private readonly Vector3[] DefaultVertices = new Vector3[4]
    {
        new(-0.5f, 0.5f, 0),
        new(0.5f, 0.5f, 0),
        new(-0.5f, -0.5f, 0),
        new(0.5f, -0.5f, 0),
    };

    public Vector3[] Vertices { get; private set; }

    /// <summary>
    /// 頂点を反映
    /// </summary>
    public void GenerateSpriteSquareMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = Vertices;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // 法線の再計算

        UvGenerate(mesh);
    }

    /// <summary>
    /// 頂点リセット
    /// </summary>
    public void ResetVertices()
    {
        Vertices = DefaultVertices.Select(v => new Vector3(v.x, v.y, v.z)).ToArray();
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateSpriteSquareMesh();
    }

    /// <summary>
    /// UVを作成
    /// </summary>
    /// <param name="mesh"></param>
    private void UvGenerate(Mesh mesh)
    {
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        mesh.uv = uv;
    }
}