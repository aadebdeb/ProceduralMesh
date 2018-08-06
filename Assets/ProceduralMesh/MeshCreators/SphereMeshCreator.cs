using UnityEngine;

namespace ProceduralMesh
{
    [ExecuteInEditMode]
    public class SphereMeshCreator : MeshCreator
    {

        [SerializeField, MoreThan(0.0f)] private float radius = 1.0f;
        [SerializeField, MoreThan(1)] private int thetaSegments = 8;
        [SerializeField, MoreThan(2)] private int phiSegments = 12;

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Sphere(radius, thetaSegments, phiSegments);
        }
    }
}