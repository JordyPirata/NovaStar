using Gameplay.Items;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif
namespace Player.Gameplay
{
    /// <summary>
    /// This attribute can only be applied to fields because its
    /// associated PropertyDrawer only operates on fields (either
    /// public or tagged with the [SerializeField] attribute) in
    /// the target MonoBehaviour.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class ItemSelectorIDAttribute : PropertyAttribute
    {

        public ItemSelectorIDAttribute()
        {

        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ItemSelectorIDAttribute))]
    public class ItemSelectorIDPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            prop.intValue = EditorGUI.Popup(position, prop.displayName, prop.intValue,  ItemsUIConfiguration.Instance.GetAllItemNames());
        }
    }
#endif
}