using UnityEditor;

namespace ProceduralMesh
{
    public abstract class MeshModifierInspector : Editor
    {

        private MeshModifier meshModifier;
        private MeshModifyApplier meshModifyApplier;

        private void OnEnable()
        {
            meshModifier = (MeshModifier)target;
            meshModifyApplier = meshModifier.GetComponent<MeshModifyApplier>();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            OnInspectorGUIInternal();
            if (EditorGUI.EndChangeCheck())
            {
                meshModifyApplier.Apply();
            }
        }

        protected virtual void OnInspectorGUIInternal()
        {
            base.OnInspectorGUI();
        }

    }
}