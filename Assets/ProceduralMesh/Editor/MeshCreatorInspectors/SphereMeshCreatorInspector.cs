using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomEditor(typeof(SphereMeshCreator))]
    public class SphereMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            OnInspectorGUIOriginal();
        }
    }
}