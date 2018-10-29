using System;
using UnityEngine;

public partial class ProceduralMesh
{

    public static Mesh InflatedPlane(Vector3 size, float exp, Vector2Int segments)
    {
        return InflatedPlane(size, exp, segments, DEFAULT_OPTION);
    }

    public static Mesh InflatedPlane(Vector3 size, float exp, Vector2Int segments, Option option)
    {
        CheckInflatedPlaneArguments(size, exp, segments);
        Vector3[] vertices = new Vector3[2 * (segments.x + 1) * (segments.y + 1)];
        int[] triangles = new int[segments.x * segments.y * 12];

        Vector3 halfSize = size * 0.5f;
        Vector2 sizeStep = new Vector2(size.x, size.y) / segments;

        int vi = 0;
        for (int y = 0; y < segments.y + 1; y++)
        {
            for (int x = 0; x < segments.x + 1; x++)
            {
                float px = sizeStep.x * x - halfSize.x;
                float py = sizeStep.y * y - halfSize.y;
                float pz = halfSize.z * (1.0f - Mathf.Pow(Mathf.Abs(px) / halfSize.x, exp)) * (1.0f - Mathf.Pow(Mathf.Abs(py) / halfSize.y, exp));
                vertices[vi++] = new Vector3(sizeStep.x * x - halfSize.x, sizeStep.y * y - halfSize.y, -pz);
            }
        }
        int ti = 0;
        for (int y = 0; y < segments.y; y++)
        {
            for (int x = 0; x < segments.x; x++)
            {
                int v00 = x + y * (segments.x + 1);
                int v10 = (x + 1) + y * (segments.x + 1);
                int v01 = x + (y + 1) * (segments.x + 1);
                int v11 = (x + 1) + (y + 1) * (segments.x + 1);
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }

        int vj = vi;
        for (int y = segments.y; y >= 0; y--)
        {
            for (int x = 0; x < segments.x + 1; x++)
            {
                float px = sizeStep.x * x - halfSize.x;
                float py = halfSize.y - sizeStep.y * y;
                float pz = halfSize.z * (1.0f - Mathf.Pow(Mathf.Abs(px) / halfSize.x, exp)) * (1.0f - Mathf.Pow(Mathf.Abs(py) / halfSize.y, exp));
                 vertices[vi++] = new Vector3(sizeStep.x * x - halfSize.x, sizeStep.y * y - halfSize.y, pz);
            }
        }
        for (int y = 0; y < segments.y; y++)
        {
            for (int x = 0; x < segments.x; x++)
            {
                int v00 = vj + x + y * (segments.x + 1);
                int v10 = vj + (x + 1) + y * (segments.x + 1);
                int v01 = vj + x + (y + 1) * (segments.x + 1);
                int v11 = vj + (x + 1) + (y + 1) * (segments.x + 1);
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }

        return finalize("Procedural InflatedPlane", vertices, triangles, option);
    }

    private static void CheckInflatedPlaneArguments(Vector3 size, float exp, Vector2Int segments)
    {
        if (size.x <= 0 || size.y <= 0 || size.z <= 0)
        {
            throw new ArgumentException(String.Format("Size must be bigger than zero: x={0}, y={1}, z={2}.", size.x, size.y, size.z));
        }
        if (exp <= 0)
        {
            throw new ArgumentException(String.Format("Exp must be bigger than zero: exp={0}.", exp));
        }
        if (segments.x <= 0 || segments.y <= 0)
        {
            throw new ArgumentException(String.Format("Segments must be bigger than two: x={0},y={1}", segments.x, segments.y));
        }
    }
}
