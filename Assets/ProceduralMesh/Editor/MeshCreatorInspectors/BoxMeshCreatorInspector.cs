using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomEditor(typeof(BoxMeshCreator))]
    public class BoxMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            base.OnInspectorGUIOriginal();
        }
    }
}
