using System;
using UnityEngine;

public partial class ProceduralMesh
{
    public static Mesh Cylinder(float height, float radius, int heightSegments, int angleSegments)
    {
        return Cylinder(height, radius, heightSegments, angleSegments, DEFAULT_OPTION);
    }

    public static Mesh Cylinder(float height, float radius, int heightSegments, int angleSegments, Option option)
    {
        CheckCylinderArguments(height, radius, heightSegments, angleSegments);
        Vector3[] vertices = new Vector3[(1 + angleSegments) * 2 + (heightSegments + 1) * angleSegments];
        int[] triangles = new int[angleSegments * 3 * 2 + heightSegments * angleSegments * 2 * 3];

        float halfHeight = height / 2.0f;
        float angleStep = Mathf.PI * 2.0f / angleSegments;
        float heightStep = height / heightSegments;
        int vi = 0;
        int vj = 0;
        int ti = 0;

        // creates bottom cap
        vertices[vj++] = new Vector3(0, -halfHeight, 0);
        for (int ai = 0; ai < angleSegments; ai++)
        {
            float angle = ai * angleStep;
            vertices[vj++] = new Vector3(radius * Mathf.Cos(angle), -halfHeight, radius * Mathf.Sin(angle));
        }
        for (int ai = 0; ai < angleSegments; ai++)
        {
            triangles[ti++] = 0;
            triangles[ti++] = ai + 1;
            triangles[ti++] = ai != angleSegments - 1 ? ai + 2 : 1;
        }
        vi += vj;

        // creates tube
        vj = 0;
        for (int hi = 0; hi < heightSegments + 1; hi++)
        {
            float h = -halfHeight + hi * heightStep;
            for (int ai = 0; ai < angleSegments; ai++)
            {
                float angle = ai * angleStep;
                vertices[vi + vj++] = new Vector3(radius * Mathf.Cos(angle), h, radius * Mathf.Sin(angle));
            }
        }
        for (int hi = 0; hi < heightSegments; hi++)
        {
            for (int ai = 0; ai < angleSegments; ai++)
            {
                int aj = ai != angleSegments - 1 ? ai + 1 : 0;
                int v00 = ai + hi * angleSegments + vi;
                int v10 = aj + hi * angleSegments + vi;
                int v01 = ai + (hi + 1) * angleSegments + vi;
                int v11 = aj + (hi + 1) * angleSegments + vi;
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }
        vi += vj;

        // creates top cap
        vj = 0;
        for (int ai = 0; ai < angleSegments; ai++)
        {
            float angle = ai * angleStep;
            vertices[vi + vj++] = new Vector3(radius * Mathf.Cos(angle), halfHeight, radius * Mathf.Sin(angle));
        }
        vertices[vi + vj++] = new Vector3(0, halfHeight, 0);
        for (int ai = 0; ai < angleSegments; ai++)
        {
            triangles[ti++] = vertices.Length - 1;
            triangles[ti++] = (ai != angleSegments - 1 ? ai + 1 : 0) + vi;
            triangles[ti++] = ai + vi;
        }

        return finalize("Procedural Cylinder", vertices, triangles, option);
    }

    private static void CheckCylinderArguments(float height, float radius, int heightSegments, int angleSegments)
    {
        if (height <= 0)
        {
            throw new ArgumentException(String.Format("Height must be bigger than zero: height={0}.", height));
        }
        if (radius <= 0)
        {
            throw new ArgumentException(String.Format("Radius must be bigger than zero: radius={0}.", radius));
        }
        if (heightSegments <= 0)
        {
            throw new ArgumentException(String.Format("Height segments must be bigger than zero: heightSegments={0}.", heightSegments));
        }
        if (angleSegments <= 2)
        {
            throw new ArgumentException(String.Format("Angle segments must be bigger than three: angleSegments={0}.", angleSegments));
        }
    }

}
