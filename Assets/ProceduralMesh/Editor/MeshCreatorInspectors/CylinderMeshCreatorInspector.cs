using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomEditor(typeof(CylinderMeshCreator))]
    public class CylinderMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            OnInspectorGUIOriginal();
        }
    }
}