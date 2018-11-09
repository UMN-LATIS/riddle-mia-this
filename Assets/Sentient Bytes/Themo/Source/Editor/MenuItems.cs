using UnityEngine;
using UnityEditor;
using System.Collections;

namespace SentientBytes.Themo
{
	public static class MenuItems
	{
		#region Public Functions
		[MenuItem("GameObject/Themo/Theme Manager", false, 10)]
		public static void CreateThemeManager(MenuCommand menuCommand)
		{
			GameObject themeManager = new GameObject("Theme Manager");
			themeManager.AddComponent<ThemeManager>();
		
			GameObjectUtility.SetParentAndAlign(themeManager, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(themeManager, "Create " + themeManager.name);
			Selection.activeGameObject = themeManager;
		}

		[MenuItem("GameObject/Themo/Theme Applier", false, 10)]
		public static void CreateThemeApplier(MenuCommand menuCommand)
		{
			GameObject themeApplier = new GameObject("Theme Applier");
			themeApplier.AddComponent<ThemeApplier>();

			GameObjectUtility.SetParentAndAlign(themeApplier, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(themeApplier, "Create " + themeApplier.name);
			Selection.activeGameObject = themeApplier;
		}
		#endregion
	}
}