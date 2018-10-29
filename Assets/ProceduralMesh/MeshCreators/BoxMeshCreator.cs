using UnityEngine;

namespace ProceduralMeshSupport
{
    public class BoxMeshCreator : MeshCreator
    {
        [SerializeField, MoreThan(0f, 0f, 0f)] private Vector3 size = new Vector3(2.0f, 1.5f, 1.0f);
        [SerializeField, MoreThan(0, 0, 0)] private Vector3Int segments = new Vector3Int(4, 3, 2);

        protected override Mesh CreateMesh()
        {
            return ProceduralMesh.Box(size, segments);
        }
    }
}