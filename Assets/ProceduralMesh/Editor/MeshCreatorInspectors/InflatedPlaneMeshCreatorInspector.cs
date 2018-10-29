using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomEditor(typeof(InflatedPlaneMeshCreator))]
    public class InflatedPlaneMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            base.OnInspectorGUIOriginal();
        }
    }
}