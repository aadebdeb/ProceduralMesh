using UnityEngine;

namespace ProceduralMesh
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
    }
}