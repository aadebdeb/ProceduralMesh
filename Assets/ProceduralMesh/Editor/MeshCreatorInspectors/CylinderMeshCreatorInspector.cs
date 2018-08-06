using UnityEditor;

namespace ProceduralMesh
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