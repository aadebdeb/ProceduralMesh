using UnityEditor;

namespace ProceduralMeshSupport
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