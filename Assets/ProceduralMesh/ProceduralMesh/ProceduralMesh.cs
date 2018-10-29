using UnityEngine;

public partial class ProceduralMesh
{
    public class Option
    {
        public bool needsNormals { get; private set;  }
        public bool needsTangents { get; private set; }
        public bool needsBounds { get; private set; }
        private Option(bool needsNormals, bool needsTangents, bool needsBounds)
        {
            this.needsNormals = needsNormals;
            this.needsTangents = needsTangents;
            this.needsBounds = needsBounds;
        }
        public class Builder
        {
            private bool needsNormals = true;
            private bool needsTangents = true;
            private bool needsBounds = true;
            public Builder NeedsNormals(bool needsNormal)
            {
                this.needsNormals = needsNormal;
                return this;
            }
            public Builder NeedsTangents(bool needsTangents)
            {
                this.needsTangents = needsTangents;
                return this;
            }
            public Builder NeedsBounds(bool needsBounds)
            {
                this.needsBounds = needsBounds;
                return this;
            }

            public Option Build()
            {
                return new Option(needsNormals, needsTangents, needsBounds);
            }
        }
    }

    public static readonly Option DEFAULT_OPTION = new Option.Builder().Build();
    public static readonly Option NO_RECALCULATION_OPTION =
        new Option.Builder().NeedsNormals(false).NeedsTangents(false).NeedsBounds(false).Build();

    private static Mesh finalize(string name, Vector3[] vertices, int[] triangles, Option option)
    {
        Mesh mesh = new Mesh();
        mesh.name = name;
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        if (option.needsNormals)
        {
            mesh.RecalculateNormals();
        }
        if (option.needsTangents)
        {
            mesh.RecalculateTangents();
        }
        if (option.needsBounds)
        {
            mesh.RecalculateBounds();
        }

        return mesh;
    }

    private static int MakeTriangle(int[] triangles, int ti, int v0, int v1, int v2)
    {
        triangles[ti] = v0;
        triangles[ti + 1] = v1;
        triangles[ti + 2] = v2;
        return ti + 3;
    }

    private static int MakeQuad(int[] triangles, int ti, int v00, int v10, int v01, int v11)
    {
        triangles[ti] = v00;
        triangles[ti + 1] = triangles[ti + 5] = v01;
        triangles[ti + 2] = triangles[ti + 4] = v10;
        triangles[ti + 3] = v11;
        return ti + 6;
    }

    public static void Scale(Vector3[] vertices, Vector3 scale)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vector3.Scale(vertices[i], scale);
        }
    }

    public static void Transform(Vector3[] vertices, Vector3 offset)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += offset;
        }
    }

    public static void DirectionalScale(Vector3[] vertices, Vector3 direction, Vector3 center, float strengh)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            float d = Vector3.Dot(v, direction);
            if (d <= 0.0f)
            {
                vertices[i] = v;
            }
            else
            {
                Vector3 toV = v - center;
                vertices[i] = v + toV * d * strengh;
            }
        }
    }

    public static void Rotate(Vector3[] vertices, Quaternion rotation)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            vertices[i] = rotation * v;
        }
    }

    public static void Twist(Vector3[] vertices, Vector3 axis, float strength, Vector3 center)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            vertices[i] = Twist(v, axis, strength, center);
        }
    }

    public static Vector3 Twist(Vector3 vertex, Vector3 axis, float strength, Vector3 center)
    {
        Vector3 dir = vertex - center;
        float len = Vector3.Dot(axis, dir);
        Quaternion rot = Quaternion.AngleAxis(len * strength, axis);
        return rot * vertex;
    }

    public static void Flatten(ref Vector3[] vertices, ref int[] triangles)
    {
        Vector3[] flatVertices = new Vector3[triangles.Length];
        int[] flatTriangles = new int[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            Vector3 v = vertices[triangles[i]];
            flatVertices[i] = new Vector3(v.x, v.y, v.z);
            flatTriangles[i] = i;
        }

        vertices = flatVertices;
        triangles = flatTriangles;
    }
}