using UnityEditor;

namespace ProceduralMesh
{
    [CustomEditor(typeof(PlaneMeshCreator))]
    public class PlaneMeshCreatorInspector : MeshCreatorInspector
    {
        protected override void OnInspectorGUIInternal()
        {
            base.OnInspectorGUIOriginal();
        }
    }
}