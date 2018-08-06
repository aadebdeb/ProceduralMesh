using UnityEditor;

namespace ProceduralMesh
{
    [CustomEditor(typeof(TorusMeshCreator))]
    public class TorusMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            OnInspectorGUIOriginal();
        }
    }
}