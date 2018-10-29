using UnityEngine;

namespace ProceduralMeshSupport
{
    public class ScaleMeshModifier : MeshModifier
    {

        [SerializeField] Vector3 scale = Vector3.one * 1.0f;

        public override void Modify(Vector3[] vertices)
        {
            ProceduralMesh.Scale(vertices, scale);
        }
    }
}
