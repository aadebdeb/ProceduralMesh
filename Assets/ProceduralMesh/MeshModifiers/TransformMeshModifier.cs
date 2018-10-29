using UnityEngine;

namespace ProceduralMeshSupport
{
    public class TransformMeshModifier : MeshModifier
    {
        [SerializeField] Vector3 offset = Vector3.zero;

        public override void Modify(Vector3[] vertices)
        {
            ProceduralMesh.Transform(vertices, offset);
        }

    }
}
