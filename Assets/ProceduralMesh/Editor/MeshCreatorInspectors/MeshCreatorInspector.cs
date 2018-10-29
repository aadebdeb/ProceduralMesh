using UnityEditor;

namespace ProceduralMeshSupport
{
    public abstract class MeshCreatorInspector : Editor
    {

        private MeshCreator meshCreator = null;

        private void OnEnable()
        {
            meshCreator = (MeshCreator)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            OnInspectorGUIInternal();
            if (EditorGUI.EndChangeCheck())
            {
                meshCreator.SetMesh();
                MeshModifyApplier meshModifyApplier = meshCreator.GetComponent<MeshModifyApplier>();
                if (meshModifyApplier != null)
                {
                    meshModifyApplier.UpdateOriginalMesh();
                }
            }
        }

        protected abstract void OnInspectorGUIInternal();

        protected void OnInspectorGUIOriginal()
        {
            base.OnInspectorGUI();
        }
    }
}
