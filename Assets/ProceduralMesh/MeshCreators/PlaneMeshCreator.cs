using UnityEngine;

namespace ProceduralMesh
{
    public class PlaneMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0f, 0f)] private Vector2 size = new Vector2(4, 3);
        [SerializeField, MoreThan(0, 0)] private Vector2Int segments = new Vector2Int(4, 3);

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Plane(size, segments);
        }
    }
}
