using UnityEngine;

namespace ProceduralMesh
{
    public class TorusMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0f)] private float majorRadius = 3f;
        [SerializeField, MoreThan(0f)] private float minorRadius = 1f;
        [SerializeField, MoreThan(2)] private int thetaSegments = 12;
        [SerializeField, MoreThan(2)] private int phiSegments = 8;

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Torus(majorRadius, minorRadius, thetaSegments, phiSegments);
        }
    }
}
