using UnityEngine;

namespace ProceduralMeshSupport
{
    public class InflatedPlaneMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0f, 0f, 0f)] private Vector3 size = new Vector3(3.0f, 2.0f, 1.0f);
        [SerializeField, MoreThan(0, 0)] private Vector2Int segments = new Vector2Int(15, 10);
        [SerializeField, MoreThan(0f)] private float exp = 2.0f;

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.InflatedPlane(size, exp, segments);
        }
    }
}
