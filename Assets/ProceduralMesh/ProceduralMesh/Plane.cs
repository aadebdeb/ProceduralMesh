using System;
using UnityEngine;

public partial class ProceduralMesh
{
    public static Mesh Plane(float xSize, float ySize, int xSegments, int ySegments)
    {
        return Plane(new Vector2(xSize, ySize), new Vector2Int(xSegments, ySegments));
    }

    public static Mesh Plane(float xSize, float ySize, int xSegments, int ySegments, Option option)
    {
        return Plane(new Vector2(xSize, ySize), new Vector2Int(xSegments, ySegments), option);
    }

    public static Mesh Plane(Vector2 size, Vector2Int segments)
    {
        return Plane(size, segments, DEFAULT_OPTION);
    }

    public static Mesh Plane(Vector2 size, Vector2Int segments, Option option)
    {
        CheckPlaneArguments(size, segments);
        Vector3[] vertices = new Vector3[(segments.x + 1) * (segments.y + 1)];
        int[] triangles = new int[segments.x * segments.y * 6];

        Vector2 halfSize = size * 0.5f;
        Vector2 sizeStep = size / segments;
        int vi = 0;
        for (int y = 0; y < segments.y + 1; y++)
        {
            for (int x = 0; x < segments.x + 1; x++)
            {
                vertices[vi++] = new Vector3(sizeStep.x * x - halfSize.x, sizeStep.y * y - halfSize.y, 0);
            }
        }
        int ti = 0;
        for (int y = 0; y < segments.y; y++)
        {
            for (int x = 0; x < segments.x; x++)
            {
                triangles[ti] = x + y * (segments.x + 1);
                triangles[ti + 1] = triangles[ti + 5] = x + (y + 1) * (segments.x + 1);
                triangles[ti + 2] = triangles[ti + 4] = (x + 1) + y * (segments.x + 1);
                triangles[ti + 3] = (x + 1) + (y + 1) * (segments.x + 1);
                ti += 6;
            }
        }

        return finalize("Procedural Plane", vertices, triangles, option);
    }

    private static void CheckPlaneArguments(Vector2 size, Vector2Int segments)
    {
        if (size.x <= 0 || size.y <= 0)
        {
            throw new ArgumentException(String.Format("Size must be bigger than zero: x={0},y={1}.", size.x, size.y));
        }
        if (segments.x <= 0 || segments.y <= 0)
        {
            throw new ArgumentException(String.Format("Segments must be bigger than two: x={0},y={1}", segments.x, segments.y));
        }
    }
}
