using UnityEngine;

namespace ProceduralMeshSupport
{
    [ExecuteInEditMode, RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class MeshCreator : MonoBehaviour
    {

        private void Awake()
        {
            SetMesh();
        }

        public void SetMesh()
        {
            GetComponent<MeshFilter>().mesh = CreateMesh();
        }

        protected abstract Mesh CreateMesh();
    }
}
