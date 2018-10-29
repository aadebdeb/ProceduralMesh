using UnityEngine;

namespace ProceduralMeshSupport
{
    public class IcosphereMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0)] private float radius = 1;
        [SerializeField, Range(0, 8)] private int divisions = 1;

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Icosphere(radius, divisions);
        }
    }
}