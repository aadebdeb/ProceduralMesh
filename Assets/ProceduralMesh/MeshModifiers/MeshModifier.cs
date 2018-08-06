using UnityEngine;

namespace ProceduralMesh
{
    [RequireComponent(typeof(MeshModifyApplier))]
    public abstract class MeshModifier : MonoBehaviour
    {
        public abstract void Modify(Vector3[] vertices);
    }
}