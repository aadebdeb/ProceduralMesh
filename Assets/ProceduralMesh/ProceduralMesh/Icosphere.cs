using UnityEngine;
using System.Collections.Generic;

public partial class ProceduralMesh
{
    public static Mesh Icosphere(float radius, int divisions)
    {
        return Icosphere(radius, divisions, DEFAULT_OPTION);
    }

    public static Mesh Icosphere(float radius, int divisions, Option option)
    {
        Vector3[] vertices = new Vector3[12];
        int[] triangles = new int[60];

        int vi = 0;

        // golden ratio
        float g = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        vertices[vi++] = new Vector3(-1, g, 0) * radius;
        vertices[vi++] = new Vector3( 1, g, 0) * radius;
        vertices[vi++] = new Vector3(-1, -g, 0) * radius;
        vertices[vi++] = new Vector3(1, -g, 0) * radius;

        vertices[vi++] = new Vector3(0, -1, g) * radius;
        vertices[vi++] = new Vector3(0, 1, g) * radius;
        vertices[vi++] = new Vector3(0, -1, -g) * radius;
        vertices[vi++] = new Vector3(0, 1, -g) * radius;

        vertices[vi++] = new Vector3(g, 0, -1) * radius;
        vertices[vi++] = new Vector3(g, 0, 1) * radius;
        vertices[vi++] = new Vector3(-g, 0, -1) * radius;
        vertices[vi++] = new Vector3(-g, 0, 1) * radius;

        int ti = 0;

        ti = MakeTriangle(triangles, ti, 0, 11, 5);
        ti = MakeTriangle(triangles, ti, 0, 5, 1);
        ti = MakeTriangle(triangles, ti, 0, 1, 7);
        ti = MakeTriangle(triangles, ti, 0, 7, 10);
        ti = MakeTriangle(triangles, ti, 0, 10, 11);

        ti = MakeTriangle(triangles, ti, 1, 5, 9);
        ti = MakeTriangle(triangles, ti, 5, 11, 4);
        ti = MakeTriangle(triangles, ti, 11, 10, 2);
        ti = MakeTriangle(triangles, ti, 10, 7, 6);
        ti = MakeTriangle(triangles, ti, 7, 1, 8);

        ti = MakeTriangle(triangles, ti, 3, 9, 4);
        ti = MakeTriangle(triangles, ti, 3, 4, 2);
        ti = MakeTriangle(triangles, ti, 3, 2, 6);
        ti = MakeTriangle(triangles, ti, 3, 6, 8);
        ti = MakeTriangle(triangles, ti, 3, 8, 9);

        ti = MakeTriangle(triangles, ti, 4, 9, 5);
        ti = MakeTriangle(triangles, ti, 2, 4, 11);
        ti = MakeTriangle(triangles, ti, 6, 2, 10);
        ti = MakeTriangle(triangles, ti, 8, 6, 7);
        ti = MakeTriangle(triangles, ti, 9, 8, 1);

        Subdivide(ref vertices, ref triangles, divisions);

        return finalize("Procedural Icosphere", vertices, triangles, option);
    }

    private static void Subdivide(ref Vector3[] vertices, ref int[] triangles, int divisions)
    {
        Dictionary<Edge, int> dividedVerticesCache = new Dictionary<Edge, int>();

        // does not use recursion to avoid high memory usage
        for (int di = 0; di < divisions; di++)
        {
            List<Vector3> dividedVertices = new List<Vector3>();
            //List<int> dividedTriangles = new List<int>();

            //Vector3[] dividedVertices = new Vector3[vertices.Length + triangles.Length];
            int[] dividedTriangles = new int[triangles.Length * 4];

            // copies original vertices
            int vi = 0;
            while (vi < vertices.Length)
            {
                dividedVertices.Add(vertices[vi++]);
            }

            // sudivides triangles
            for (int ti = 0, tj = 0; tj < triangles.Length; tj += 3)
            {
                int i0 = triangles[tj];
                int i1 = triangles[tj + 1];
                int i2 = triangles[tj + 2];
                Vector3 v0 = vertices[i0];
                Vector3 v1 = vertices[i1];
                Vector3 v2 = vertices[i2];
                Edge e01 = new Edge(i0, i1);
                Edge e12 = new Edge(i1, i2);
                Edge e20 = new Edge(i2, i0);
                int ia, ib, ic;
                if (dividedVerticesCache.ContainsKey(e01))
                {
                    ia = dividedVerticesCache[e01];
                }
                else
                {
                    ia = vi++;
                    dividedVertices.Add(Vector3.Slerp(v0, v1, 0.5f));
                    dividedVerticesCache.Add(e01, ia);
                }
                if (dividedVerticesCache.ContainsKey(e12))
                {
                    ib = dividedVerticesCache[e12];
                }
                else
                {
                    ib = vi++;
                    dividedVertices.Add(Vector3.Slerp(v1, v2, 0.5f));
                    dividedVerticesCache.Add(e12, ib);
                }
                if (dividedVerticesCache.ContainsKey(e20))
                {
                    ic = dividedVerticesCache[e20];
                }
                else
                {
                    ic = vi++;
                    dividedVertices.Add(Vector3.Slerp(v2, v0, 0.5f));
                    dividedVerticesCache.Add(e20, ic);
                }

                ti = MakeTriangle(dividedTriangles, ti, i0, ia, ic);
                ti = MakeTriangle(dividedTriangles, ti, i1, ib, ia);
                ti = MakeTriangle(dividedTriangles, ti, i2, ic, ib);
                ti = MakeTriangle(dividedTriangles, ti, ia, ib, ic);
            }

            dividedVerticesCache.Clear();

            vertices = dividedVertices.ToArray();
            triangles = dividedTriangles;

        }
    }

    public class Edge
    {
        private int v0, v1;

        public Edge(int v0, int v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }

        public override int GetHashCode()
        {
            return v0.GetHashCode() ^ v1.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Edge other = obj as Edge;
            if (other == null) return false;

            return (v0 == other.v0 && v1 == other.v1) || (v1 == other.v0 && v0 == other.v1);
        }
    }
}
