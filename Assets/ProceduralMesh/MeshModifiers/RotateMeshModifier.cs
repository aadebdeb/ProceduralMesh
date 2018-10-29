using UnityEngine;

namespace ProceduralMeshSupport
{
    public class RotateMeshModifier : MeshModifier {

        [SerializeField] Vector3 rotation = Vector3.zero;

        public override void Modify(Vector3[] vertices)
        {
            Quaternion quaternion = Quaternion.Euler(rotation);
            ProceduralMesh.Rotate(vertices, quaternion);
        }
    }
}