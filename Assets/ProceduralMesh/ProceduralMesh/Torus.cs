using System;
using UnityEngine;

namespace ProceduralMesh
{
    public partial class ProceduralMesh
    {
        public static Mesh Torus(float majorRadius, float minorRadius, int thetaSegments, int phiSegments)
        {
            return Torus(majorRadius, minorRadius, thetaSegments, phiSegments, DEFAULT_OPTION);
        }

        public static Mesh Torus(float majorRadius, float minorRadius, int thetaSegments, int phiSegments, Option option)
        {
            CheckTorusArguments(majorRadius, minorRadius, thetaSegments, phiSegments);

            Vector3[] vertices = new Vector3[thetaSegments * phiSegments];
            int[] triangles = new int[thetaSegments * phiSegments * 2 * 3];

            // creates vertices
            float thetaStep = Mathf.PI * 2.0f / thetaSegments;
            float phiStep = Mathf.PI * 2.0f / phiSegments;
            int vi = 0;
            for (int hi = 0; hi < thetaSegments; hi++)
            {
                Quaternion rot = Quaternion.AngleAxis(hi * thetaStep * Mathf.Rad2Deg, Vector3.up);
                for (int pi = 0; pi < phiSegments; pi++)
                {
                    float phi = pi * phiStep;
                    vertices[vi++] = rot * new Vector3(majorRadius + minorRadius * Mathf.Cos(phi), minorRadius * Mathf.Sin(phi), 0);
                }
            }

            // creates triangles
            int ti = 0;
            for (int hi = 0; hi < thetaSegments; hi++)
            {
                int hj = hi != thetaSegments - 1 ? hi + 1 : 0;
                for (int pi = 0; pi < phiSegments; pi++)
                {
                    int pj = pi != phiSegments - 1 ? pi + 1 : 0;
                    int v00 = pi + hi * phiSegments;
                    int v10 = pj + hi * phiSegments;
                    int v01 = pi + hj * phiSegments;
                    int v11 = pj + hj * phiSegments;
                    ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
                }
            }

            return finalize("Procedural Torus", vertices, triangles, option);
        }

        private static void CheckTorusArguments(float majorRadius, float minorRadius, int thetaSegments, int phiSegments)
        {
            if (majorRadius <= 0)
            {
                throw new ArgumentException(String.Format("Major radius must be bigger than zero: majorRadius={0}.", majorRadius));
            }
            if (minorRadius <= 0)
            {
                throw new ArgumentException(String.Format("Minor radius must be bigger than zero: minorRadius={0}.", minorRadius));
            }
            if (thetaSegments <= 2)
            {
                throw new ArgumentException(String.Format("Theta segments must be bigger than two: thetaSegments={0}.", thetaSegments));
            }
            if (phiSegments <= 2)
            {
                throw new ArgumentException(String.Format("Angle segments must be bigger than two: phiSegments={0}.", phiSegments));
            }
        }
    }
}
