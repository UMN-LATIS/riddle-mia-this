using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SentientBytes.Themo
{
	[CustomEditor(typeof(ThemeApplier))]
	public class ThemeApplierEditor : Editor
	{
		SerializedProperty ID, ApplyInUpdate, CustomLightness, CustomSaturation, CustomHue, HueIsAbsolute, CustomAlpha, Alpha, Lightness, Saturation, Hue, OnApplyTheme;
		ThemeManager themeManager;
		ThemeApplier themeApplier;

		int managerCount;

		void OnEnable()
		{
			managerCount = FindObjectsOfType<ThemeManager>().Length;

			if (managerCount == 1)
			{
				themeApplier = target as ThemeApplier;
				themeManager = FindObjectOfType<ThemeManager>();

				ID = serializedObject.FindProperty("ID");

				ApplyInUpdate = serializedObject.FindProperty("ApplyInUpdate");

				CustomAlpha = serializedObject.FindProperty("CustomAlpha");
				CustomLightness = serializedObject.FindProperty("CustomLightness");
				CustomSaturation = serializedObject.FindProperty("CustomSaturation");
				CustomHue = serializedObject.FindProperty("CustomHue");
				HueIsAbsolute = serializedObject.FindProperty("HueIsAbsolute");

				Alpha = serializedObject.FindProperty("Alpha");
				Lightness = serializedObject.FindProperty("Lightness");
				Saturation = serializedObject.FindProperty("Saturation");
				Hue = serializedObject.FindProperty("Hue");

				OnApplyTheme = serializedObject.FindProperty("OnApplyTheme");
			}
		}

		public override void OnInspectorGUI()
		{
			if (managerCount == 1)
			{
				serializedObject.Update();
				EditorGUILayout.Space();

				bool themeManagerPresent = themeManager && themeManager.Themes.Count > 0;

				if (themeManagerPresent && themeManager.Themes.FindIndex(t => t.ID == ID.stringValue) != -1)
				{
					GUI.backgroundColor = themeApplier.BaseColor;
					int currentIndex = themeManager.Themes.FindIndex(t => t.ID == ID.stringValue);
					int popupIndex = EditorGUILayout.Popup("Theme", currentIndex, themeManager.ThemeNames.ToArray());
					ID.stringValue = themeManager.Themes[popupIndex].ID;
					GUI.backgroundColor = Color.white;
					EditorGUILayout.ColorField("Color", themeApplier.BaseColor);

					if (CustomAlpha.boolValue || CustomLightness.boolValue || CustomSaturation.boolValue || CustomHue.boolValue)
						EditorGUILayout.ColorField("Custom", themeApplier.CalculatedColor);
					else
						GUILayout.Space(18f);
				}
				else
				{
					if (!themeManagerPresent)
						EditorGUILayout.HelpBox("Make sure there is a Theme Manager with at least 1 Theme present", MessageType.Error);
					EditorGUILayout.HelpBox("ID \"" + ID.stringValue + "\" was not found", MessageType.Error);
					EditorGUILayout.PropertyField(ID);
				}

				EditorGUILayout.Space();

				EditorGUILayout.PropertyField(ApplyInUpdate);

				EditorGUILayout.Space();

				EditorGUILayout.PropertyField(CustomAlpha);
				if (CustomAlpha.boolValue)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(Alpha);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.PropertyField(CustomLightness);
				if (CustomLightness.boolValue)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(Lightness);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.PropertyField(CustomSaturation);
				if (CustomSaturation.boolValue)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(Saturation);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.PropertyField(CustomHue);
				if (CustomHue.boolValue)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(HueIsAbsolute, new GUIContent("Absolute"));
					EditorGUILayout.PropertyField(Hue);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.Space();

				EditorGUILayout.PropertyField(OnApplyTheme);

				EditorGUILayout.Space();

				serializedObject.ApplyModifiedProperties();
			}
			else
				EditorGUILayout.HelpBox("There needs to be exactly 1 Theme Manager present in the scene. You have " + managerCount + ".", MessageType.Error);
		}
	}
}