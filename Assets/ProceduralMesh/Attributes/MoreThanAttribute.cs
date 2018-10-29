using UnityEngine;

namespace ProceduralMeshSupport
{
    public class MoreThanAttribute : PropertyAttribute
    {

        public readonly int integerValue;
        public MoreThanAttribute(int integerValue)
        {
            this.integerValue = integerValue;
        }

        public readonly float floatValue;
        public MoreThanAttribute(float floatValue)
        {
            this.floatValue = floatValue;
        }

        public readonly Vector2 vector2Value;
        public MoreThanAttribute(float xValue, float yValue)
        {
            this.vector2Value = new Vector2(xValue, yValue);
        }

        public readonly Vector2Int vector2IntValue;
        public MoreThanAttribute(int xValue, int yValue)
        {
            this.vector2IntValue = new Vector2Int(xValue, yValue);
        }

        public readonly Vector3 vector3Value;
        public MoreThanAttribute(float xValue, float yValue, float zValue)
        {
            this.vector3Value = new Vector3(xValue, yValue, zValue);
        }

        public readonly Vector3Int vector3IntValue;
        public MoreThanAttribute(int xValue, int yValue, int zValue)
        {
            this.vector3IntValue = new Vector3Int(xValue, yValue, zValue);
        }
    }
}