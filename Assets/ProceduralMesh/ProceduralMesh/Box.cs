using System;
using UnityEngine;

public partial class ProceduralMesh
{
    public static Mesh Box(Vector3 size, int xSegments, int ySegments, int zSegments)
    {
        return Box(size.x, size.y, size.z, xSegments, ySegments, zSegments);
    }

    public static Mesh Box(Vector3 size, int xSegments, int ySegments, int zSegments, Option option)
    {
        return Box(size.x, size.y, size.z, xSegments, ySegments, zSegments, option);
    }

    public static Mesh Box(float xSize, float ySize, float zSize, Vector3Int segments)
    {
        return Box(xSize, ySize, zSize, segments.x, segments.y, segments.z);
    }

    public static Mesh Box(float xSize, float ySize, float zSize, Vector3Int segments, Option option)
    {
        return Box(xSize, ySize, zSize, segments.x, segments.y, segments.z, option);
    }

    public static Mesh Box(Vector3 size, Vector3Int segments)
    {
        return Box(size.x, size.y, size.z, segments.x, segments.y, segments.z);
    }

    public static Mesh Box(Vector3 size, Vector3Int segments, Option option)
    {
        return Box(size.x, size.y, size.z, segments.x, segments.y, segments.z, option);
    }

    public static Mesh Box(float xSize, float ySize, float zSize, int xSegments, int ySegments, int zSegments)
    {
        return Box(xSize, ySize, zSize, xSegments, ySegments, zSegments, DEFAULT_OPTION);
    }

    public static Mesh Box(float xSize, float ySize, float zSize, int xSegments, int ySegments, int zSegments, Option option)
    {
        CheckBoxArguments(xSize, ySize, zSize, xSegments, ySegments, zSegments);
        Vector3[] vertices = new Vector3[2 * ((xSegments + 1) * (ySegments + 1) + (ySegments + 1) * (zSegments + 1) + (zSegments + 1) * (xSegments + 1))];
        int[] triangles = new int[(xSegments * ySegments + ySegments * zSegments + zSegments * xSegments) * 12];

        int vi = 0;
        int ti = 0;
        MakeNearPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, xSegments, ySegments);
        MakeRightPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, zSegments, ySegments);
        MakeFarPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, xSegments, ySegments);
        MakeLeftPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, zSegments, ySegments);
        MakeUpPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, xSegments, zSegments);
        MakeDownPlane(vertices, triangles, ref vi, ref ti, xSize, ySize, zSize, xSegments, zSegments);

        return finalize("Procedural Box", vertices, triangles, option);
    }

    private static void MakeNearPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int xSegments, int ySegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float xStep = xSize / xSegments;
        float yStep = ySize / ySegments;

        var vj = 0;
        for (int y = 0; y < ySegments + 1; y++)
        {
            for (int x = 0; x < xSegments + 1; x++)
            {
                vertices[vi + vj++] = new Vector3(xStep * x - xHalf, yStep * y - yHalf, -zHalf); ;
            }
        }
        for (int y = 0; y < ySegments; y++)
        {
            for (int x = 0; x < xSegments; x++)
            {
                int v00 = vi + x + y * (xSegments + 1);
                int v10 = vi + (x + 1) + y * (xSegments + 1);
                int v01 = vi + x + (y + 1) * (xSegments + 1);
                int v11 = vi + (x + 1) + (y + 1) * (xSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }
        vi += vj;
    }

    private static void MakeRightPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int zSegments, int ySegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float zStep = zSize / zSegments;
        float yStep = ySize / ySegments;

        var vj = 0;
        for (int y = 0; y < ySegments + 1; y++)
        {
            for (int z = 0; z < zSegments + 1; z++)
            {
                vertices[vi + vj++] = new Vector3(xHalf, yStep * y - yHalf, zStep * z - zHalf); ;
            }
        }
        for (int y = 0; y < ySegments; y++)
        {
            for (int z = 0; z < zSegments; z++)
            {
                int v00 = vi + z + y * (zSegments + 1);
                int v10 = vi + (z + 1) + y * (zSegments + 1);
                int v01 = vi + z + (y + 1) * (zSegments + 1);
                int v11 = vi + (z + 1) + (y + 1) * (zSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }
        vi += vj;
    }

    private static void MakeFarPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int xSegments, int ySegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float xStep = xSize / xSegments;
        float yStep = ySize / ySegments;

        var vj = 0;
        for (int y = 0; y < ySegments + 1; y++)
        {
            for (int x = xSegments; x >= 0; x--)
            {
                vertices[vi + vj++] = new Vector3(xHalf - xStep * x, yStep * y - yHalf, zHalf); ;
            }
        }
        for (int y = 0; y < ySegments; y++)
        {
            for (int x = 0; x < xSegments; x++)
            {
                int v00 = vi + x + y * (xSegments + 1);
                int v10 = vi + (x + 1) + y * (xSegments + 1);
                int v01 = vi + x + (y + 1) * (xSegments + 1);
                int v11 = vi + (x + 1) + (y + 1) * (xSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v01, v10, v11);
            }
        }
        vi += vj;
    }

    private static void MakeLeftPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int zSegments, int ySegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float zStep = zSize / zSegments;
        float yStep = ySize / ySegments;

        var vj = 0;
        for (int y = 0; y < ySegments + 1; y++)
        {
            for (int z = zSegments; z >= 0; z--)
            {
                vertices[vi + vj++] = new Vector3(-xHalf, yStep * y - yHalf, zHalf - zStep * z); ;
            }
        }
        for (int y = 0; y < ySegments; y++)
        {
            for (int z = 0; z < zSegments; z++)
            {
                int v00 = vi + z + y * (zSegments + 1);
                int v10 = vi + (z + 1) + y * (zSegments + 1);
                int v01 = vi + z + (y + 1) * (zSegments + 1);
                int v11 = vi + (z + 1) + (y + 1) * (zSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v01, v10, v11);
            }
        }
        vi += vj;
    }

    private static void MakeUpPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int xSegments, int zSegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float xStep = xSize / xSegments;
        float zStep = zSize / zSegments;

        var vj = 0;
        for (int z = 0; z < zSegments + 1; z++)
        {
            for (int x = 0; x < xSegments + 1; x++)
            {
                vertices[vi + vj++] = new Vector3(xStep * x - xHalf, yHalf, zStep * z - zHalf);
            }
        }
        for (int z = 0; z < zSegments; z++)
        {
            for (int x = 0; x < xSegments; x++)
            {
                int v00 = vi + x + z * (xSegments + 1);
                int v10 = vi + (x + 1) + z * (xSegments + 1);
                int v01 = vi + x + (z + 1) * (xSegments + 1);
                int v11 = vi + (x + 1) + (z + 1) * (xSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }
        vi += vj;
    }

    private static void MakeDownPlane(Vector3[] vertices, int[] triangles, ref int vi, ref int ti, float xSize, float ySize, float zSize, int xSegments, int zSegments)
    {
        float xHalf = xSize * 0.5f;
        float yHalf = ySize * 0.5f;
        float zHalf = zSize * 0.5f;
        float xStep = xSize / xSegments;
        float zStep = zSize / zSegments;

        var vj = 0;
        for (int z = zSegments; z >= 0; z--)
        {
            for (int x = 0; x < xSegments + 1; x++)
            {
                vertices[vi + vj++] = new Vector3(xStep * x - xHalf, -yHalf, zHalf - zStep * z);
            }
        }
        for (int z = 0; z < zSegments; z++)
        {
            for (int x = 0; x < xSegments; x++)
            {
                int v00 = vi + x + z * (xSegments + 1);
                int v10 = vi + (x + 1) + z * (xSegments + 1);
                int v01 = vi + x + (z + 1) * (xSegments + 1);
                int v11 = vi + (x + 1) + (z + 1) * (xSegments + 1);
                ti = MakeQuad(triangles, ti, v00, v01, v10, v11);
            }
        }
        vi += vj;
    }

    private static void CheckBoxArguments(float xSize, float ySize, float zSize, int xSegments, int ySegments, int zSegments)
    {
        if (xSize <= 0)
        {
            throw new ArgumentException(String.Format("X size must be bigger than zero: xSize={0}.", xSize));
        }
        if (ySize <= 0)
        {
            throw new ArgumentException(String.Format("Y size must be bigger than zero: ySize={0}.", ySize));
        }
        if (zSize <= 0)
        {
            throw new ArgumentException(String.Format("Z size must be bigger than zero: zSize={0}.", zSize));
        }
        if (xSegments <= 0)
        {
            throw new ArgumentException(String.Format("X segments must be bigger than zero: xSegments={0}.", xSegments));
        }
        if (ySegments <= 0)
        {
            throw new ArgumentException(String.Format("Y segments must be bigger than zero: ySegments={0}.", ySegments));
        }
        if (zSegments <= 0)
        {
            throw new ArgumentException(String.Format("Z segments must be bigger than zero: zSegments={0}.", zSegments));
        }
    }

}
