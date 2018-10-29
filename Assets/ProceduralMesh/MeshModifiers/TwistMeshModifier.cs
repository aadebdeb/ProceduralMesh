using UnityEngine;

namespace ProceduralMeshSupport
{
    public class TwistMeshModifier : MeshModifier
    {
        [SerializeField] private Vector3 axis = Vector3.up;
        [SerializeField] private float strength = 1f;
        [SerializeField] private Vector3 center = Vector3.zero;

        public override void Modify(Vector3[] vertices)
        {
            ProceduralMesh.Twist(vertices, axis.normalized, strength, center);
        }
    }
}
