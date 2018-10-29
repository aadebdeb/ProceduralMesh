using System.Collections.Generic;
using UnityEngine;

namespace ProceduralMeshSupport
{
    [ExecuteInEditMode, RequireComponent(typeof(MeshFilter))]
    public class MeshModifyApplier : MonoBehaviour
    {
        private Mesh originalMesh;

        private void OnEnable()
        {
            originalMesh = GetComponent<MeshFilter>().sharedMesh;
            Apply();
        }

#if UNITY_EDITOR
        public void UpdateOriginalMesh()
        {
            originalMesh = GetComponent<MeshFilter>().sharedMesh;
            Apply();
        }
#endif

        public void Apply()
        {
            List<MeshModifier> meshModifiers = new List<MeshModifier>();
            GetComponents(meshModifiers);
            if (meshModifiers.Count == 0) return;

            Mesh modifiedMesh = Instantiate<Mesh>(originalMesh);
            Vector3[] vertices = modifiedMesh.vertices;
            foreach (MeshModifier meshModifier in meshModifiers)
            {
                meshModifier.Modify(vertices);
            }
            modifiedMesh.vertices = vertices;

            GetComponent<MeshFilter>().mesh = modifiedMesh;
        }
    }
}
