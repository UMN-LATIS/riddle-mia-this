using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

namespace SentientBytes.Themo
{
	[CustomEditor(typeof(ThemeManager))]
	public class ThemeManagerEditor : Editor
	{
		SerializedProperty Themes, ApplyInUpdate;
		ReorderableList themes;
		int managerCount;

		void OnEnable()
		{
			managerCount = FindObjectsOfType<ThemeManager>().Length;

			if (managerCount == 1)
			{
				Themes = serializedObject.FindProperty("Themes");
				ApplyInUpdate = serializedObject.FindProperty("ApplyInUpdate");

				themes = new ReorderableList(serializedObject, Themes, true, true, true, true);
				themes.elementHeight = EditorGUIUtility.singleLineHeight * 4.25f;

				themes.drawHeaderCallback = (Rect rect) =>
				{
					EditorGUI.LabelField(rect, "Themes");
				};

				themes.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
				{
					SerializedProperty element = themes.serializedProperty.GetArrayElementAtIndex(index);
					rect.y += 2f;

					float lineHeight = EditorGUIUtility.singleLineHeight;
					float nextLineHeight = lineHeight * 1.25f;

					EditorGUI.LabelField(new Rect(rect.x, rect.y, 50f, lineHeight), new GUIContent("Color"));
					EditorGUI.PropertyField(new Rect(rect.x + 60f, rect.y, rect.width - 60f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Color"), GUIContent.none);

					EditorGUI.LabelField(new Rect(rect.x, rect.y + nextLineHeight, 50f, lineHeight), new GUIContent("Name"));
					EditorGUI.PropertyField(new Rect(rect.x + 60f, rect.y + nextLineHeight, rect.width - 78f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);

					EditorGUI.LabelField(new Rect(rect.x, rect.y + nextLineHeight * 2f, 50f, lineHeight), new GUIContent("ID"));
					EditorGUI.PropertyField(new Rect(rect.x + 60f, rect.y + nextLineHeight * 2f, rect.width - 78f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ID"), GUIContent.none);
				};

				themes.onRemoveCallback = (ReorderableList l) =>
				{
					if (l.count > 1)
					{
						ReorderableList.defaultBehaviours.DoRemoveButton(l);
						if (l.count == 1)
							l.displayRemove = false;
					}
				};

				themes.onAddCallback = (ReorderableList l) =>
				{
					int index = l.serializedProperty.arraySize;
					l.serializedProperty.arraySize++;
					l.index = index;
					SerializedProperty element = l.serializedProperty.GetArrayElementAtIndex(index);
					element.FindPropertyRelative("ID").stringValue = ThemeManager.Theme.NewID();
					element.FindPropertyRelative("Name").stringValue = "Blank " + index;
					element.FindPropertyRelative("Color").colorValue = Color.white;

					if (l.count > 1)
						l.displayRemove = true;
				};
			}
		}

		public override void OnInspectorGUI()
		{
			if (managerCount == 1)
			{
				serializedObject.Update();
				EditorGUILayout.Space();

				EditorGUILayout.PropertyField(ApplyInUpdate);

				EditorGUILayout.Space();

				themes.DoLayoutList();

				EditorGUILayout.Space();
				serializedObject.ApplyModifiedProperties();
			}
			else
				EditorGUILayout.HelpBox("There needs to be exactly 1 Theme Manager present in the scene. You have " + managerCount + ".", MessageType.Error);
		}
	}
}