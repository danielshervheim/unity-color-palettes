using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace DSS.ColorPalettes
{
    [CustomEditor(typeof(ApplyColorPaletteToButton))]
    public class ApplyColorPaletteToButtonEditor : Editor
    {
        private SerializedProperty m_preset;
        private SerializedProperty m_targets;
        private ReorderableList m_targetsList;

        private SerializedProperty m_lerpCurve;
        private SerializedProperty m_lerpDuration;

        private void OnEnable()
        {
            m_preset = serializedObject.FindProperty("preset");
            m_targets = serializedObject.FindProperty("targets");
            m_targetsList = new ReorderableList(
                serializedObject: serializedObject,
                elements: m_targets,
                draggable: true,
                displayHeader: true,
                displayAddButton: true,
                displayRemoveButton: true
            );
            m_targetsList.drawHeaderCallback = DrawHeaderCallback;
            m_targetsList.drawElementCallback = DrawElementCallback;
            m_targetsList.elementHeightCallback += ElementHeightCallback;
            m_targetsList.onAddCallback += OnAddCallback;

            m_lerpCurve = serializedObject.FindProperty("lerpCurve");
            m_lerpDuration = serializedObject.FindProperty("lerpDuration");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Preset", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_preset);
            EditorGUILayout.Space();

            m_targetsList.DoLayoutList();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Lerp Behaviour", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_lerpCurve, new GUIContent("Curve"));
            EditorGUILayout.PropertyField(m_lerpDuration, new GUIContent("Duration"));

            serializedObject.ApplyModifiedProperties();
        }

        // @brief Draws the header for the reorderable list
        private void DrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, "Targets");
        }

        // @brief This methods decides how to draw each element in the list
        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            // Get the element we want to draw from the list.
            SerializedProperty element = m_targetsList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // We get the name property of our element so we can display this in our list.
            string elementTitle = "Unreferenced Target";
            Graphic g = (Graphic)element.FindPropertyRelative("graphic").objectReferenceValue;
            if (g != null)
            {
                elementTitle = g.gameObject.name;
            }

            // Draw the list item as a property field, just like Unity does internally.
            int leftPadding = 15;
            EditorGUI.PropertyField(
                // position: new Rect(rect.x += 10, rect.y, Screen.width * 0.8f, height: EditorGUIUtility.singleLineHeight),
                position: new Rect(
                    rect.x+leftPadding,
                    rect.y,
                    rect.width-leftPadding,
                    EditorGUIUtility.singleLineHeight
                ),
                property: element,
                label: new GUIContent(elementTitle),
                includeChildren: true
            );
        }

        // @brief Calculates the height of a single element in the list.
        // This is extremely useful when displaying list-items with nested data.
        private float ElementHeightCallback(int index)
        {
            // Gets the height of the element. This also accounts for properties that can be expanded, like structs.
            float propertyHeight = EditorGUI.GetPropertyHeight(m_targetsList.serializedProperty.GetArrayElementAtIndex(index), true);
            float spacing = EditorGUIUtility.singleLineHeight / 2;
            return propertyHeight + spacing;
        }

        // @brief Defines how a new list element should be created and added to our list.
        private void OnAddCallback(ReorderableList list)
        {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
        }
    }
}