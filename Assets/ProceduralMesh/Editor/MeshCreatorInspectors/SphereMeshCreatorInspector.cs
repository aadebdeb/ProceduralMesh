using UnityEditor;

namespace ProceduralMesh
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