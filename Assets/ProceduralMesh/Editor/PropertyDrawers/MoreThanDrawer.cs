using UnityEngine;
using UnityEditor;

namespace ProceduralMeshSupport
{
    [CustomPropertyDrawer(typeof(MoreThanAttribute))]
    public class MoreThanDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MoreThanAttribute moreThanAttribute = (MoreThanAttribute)attribute;
            EditorGUI.PropertyField(position, property, label);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                if (property.intValue <= moreThanAttribute.integerValue)
                {
                    property.intValue = moreThanAttribute.integerValue + 1;
                }
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                if (property.floatValue <= moreThanAttribute.floatValue)
                {
                    property.floatValue = moreThanAttribute.floatValue + Mathf.Epsilon;
                }
            }
            else if (property.propertyType == SerializedPropertyType.Vector2)
            {
                Vector2 attributeValue = moreThanAttribute.vector2Value;
                Vector2 propertyValue = property.vector2Value;
                property.vector2Value = new Vector2(
                    propertyValue.x <= attributeValue.x ? attributeValue.x + Mathf.Epsilon : propertyValue.x,
                    propertyValue.y <= attributeValue.y ? attributeValue.y + Mathf.Epsilon : propertyValue.y
                );
            }
            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                Vector2Int attributeValue = moreThanAttribute.vector2IntValue;
                Vector2Int propertyValue = property.vector2IntValue;
                property.vector2IntValue = new Vector2Int(
                    propertyValue.x <= attributeValue.x ? attributeValue.x + 1 : propertyValue.x,
                    propertyValue.y <= attributeValue.y ? attributeValue.y + 1 : propertyValue.y
                );
            }
            else if (property.propertyType == SerializedPropertyType.Vector3)
            {
                Vector3 attributeValue = moreThanAttribute.vector3Value;
                Vector3 propertyValue = property.vector3Value;
                property.vector3Value = new Vector3(
                    propertyValue.x <= attributeValue.x ? attributeValue.x + Mathf.Epsilon : propertyValue.x,
                    propertyValue.y <= attributeValue.y ? attributeValue.y + Mathf.Epsilon : propertyValue.y,
                    propertyValue.z <= attributeValue.z ? attributeValue.z + Mathf.Epsilon : propertyValue.z
                );
            }
            else if (property.propertyType == SerializedPropertyType.Vector3Int)
            {
                Vector3Int attributeValue = moreThanAttribute.vector3IntValue;
                Vector3Int propertyValue = property.vector3IntValue;
                property.vector3IntValue = new Vector3Int(
                    propertyValue.x <= attributeValue.x ? attributeValue.x + 1 : propertyValue.x,
                    propertyValue.y <= attributeValue.y ? attributeValue.y + 1 : propertyValue.y,
                    propertyValue.z <= attributeValue.z ? attributeValue.z + 1 : propertyValue.z
                );
            }
        }
    }
}