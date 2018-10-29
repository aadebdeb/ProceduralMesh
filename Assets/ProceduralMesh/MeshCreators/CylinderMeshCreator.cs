using UnityEngine;

namespace ProceduralMeshSupport
{
    public class CylinderMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0f)] float height = 2.0f;
        [SerializeField, MoreThan(0f)] float radius = 1.0f;
        [SerializeField, MoreThan(0)] int heightSegments = 5;
        [SerializeField, MoreThan(2)] int angleSegments = 8;

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Cylinder(height, radius, heightSegments, angleSegments);
        }
    }
}