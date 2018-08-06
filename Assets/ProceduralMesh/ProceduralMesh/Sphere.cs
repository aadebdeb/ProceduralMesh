using System;
using UnityEngine;

namespace ProceduralMesh
{
    public partial class ProceduralMesh
    {

        public static Mesh Sphere(float radius, int thetaSegments, int phiSegments)
        {
            return Sphere(radius, thetaSegments, phiSegments, DEFAULT_OPTION);
        }

        public static Mesh Sphere(float radius, int thetaSegments, int phiSegments, Option option)
        {
            CheckSphereArguments(radius, thetaSegments, phiSegments);
            Vector3[] vertices = new Vector3[2 + (thetaSegments - 1) * phiSegments];
            int[] triangles = new int[(phiSegments) * 2 * 3 + (thetaSegments - 2) * phiSegments * 2 * 3];

            float thetaStep = Mathf.PI / thetaSegments;
            float phiStep = Mathf.PI * 2.0f / phiSegments;
            int vi = 0;
            int ti = 0;

            vertices[vi++] = new Vector3(0, -1, 0) * radius;
            for (int hi = 1; hi < thetaSegments; hi++)
            {
                float theta = Mathf.PI - hi * thetaStep;
                for (int pi = 0; pi < phiSegments; pi++)
                {
                    float phi = pi * phiStep;
                    vertices[vi++] = new Vector3(Mathf.Sin(theta) * Mathf.Cos(phi), Mathf.Cos(theta), Mathf.Sin(theta) * Mathf.Sin(phi)) * radius;
                }
            }
            vertices[vi++] = new Vector3(0, 1, 0) * radius;

            for (int pi = 0; pi < phiSegments; pi++)
            {
                triangles[ti++] = 0;
                triangles[ti++] = pi + 1;
                triangles[ti++] = pi != phiSegments - 1 ? (pi + 1) + 1 : 1;
            }
            for (int hi = 0; hi < thetaSegments - 2; hi++)
            {
                for (int pi = 0; pi < phiSegments; pi++)
                {
                    int pj = pi != phiSegments - 1 ? pi + 1 : 0;
                    int v00 = pi + hi * phiSegments + 1;
                    int v10 = pj + hi * phiSegments + 1;
                    int v01 = pi + (hi + 1) * phiSegments + 1;
                    int v11 = pj + (hi + 1) * phiSegments + 1;
                    ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
                }
            }
            for (int pi = 0; pi < phiSegments; pi++)
            {
                triangles[ti++] = vertices.Length - 1;
                triangles[ti++] = (pi != phiSegments - 1 ? pi + 1 : 0) + (thetaSegments - 2) * phiSegments + 1;
                triangles[ti++] = pi + (thetaSegments - 2) * phiSegments + 1;
            }

            return finalize("Procedural Sphere", vertices, triangles, option);
        }

        private static void CheckSphereArguments(float radius, int thetaSegments, int phiSegments)
        {
            if (radius <= 0)
            {
                throw new ArgumentException(String.Format("Radius must be bigger than zero: radius={0}.", radius));
            }
            if (thetaSegments <= 1)
            {
                throw new ArgumentException(String.Format("ThetaSegments must be bigger than one: thetaSegments={0}.", thetaSegments));
            }
            if (phiSegments <= 2)
            {
                throw new ArgumentException(String.Format("PhiSegments must be bigger than two: phiSegments={0}.", phiSegments));
            }
        }
    }
}