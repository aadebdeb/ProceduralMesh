using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomEditor(typeof(IcosphereMeshCreator))]
    public class IcosphereMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            base.OnInspectorGUIOriginal();
        }
    }
}