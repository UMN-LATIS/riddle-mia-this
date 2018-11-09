using UnityEngine;
using System.Collections.Generic;
using System;
using SentientBytes.Helpers;

namespace SentientBytes.Themo
{
	[AddComponentMenu("Sentient Bytes/Themo/Theme Manager"), DisallowMultipleComponent]
	public class ThemeManager : Singleton<ThemeManager>
	{
		#region Nested Types
		/// <summary>
		/// Represents a color theme
		/// </summary>
		[Serializable]
		public class Theme
		{
			public string ID;
			public string Name;
			public Color Color;

			static string symbols = "0123456789qwertzuiopasdfghjklyxcvbnmQWERTZUIOPASDFGHJKLYXCVBNM";
			public static string NewID()
			{
				string id = "";
				System.Random rand = new System.Random();
				for (int i = 0; i < 5; i++)
					id += symbols[rand.Next(0, symbols.Length)];
				return id;
			}

			public Theme(string id, string name, Color color)
			{
				ID = id;
				Name = name;
				Color = color;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// This event is triggered in UpdateThemes(), and is used to propagate any modifications to the theme colors
		/// </summary>
		public event Action OnApplyThemes;
		#endregion

		#region Public Vars
		/// <summary>
		/// For performance sake, the Theme Applier script does not update the colors of its listeners each frame by default.
		/// Set this to true to update Theme Appliers every frame. Can be useful to tweak your themes during play mode
		/// </summary>
		public bool ApplyInUpdate = false;

		/// <summary>
		/// A list of themes that Theme Appliers can use.
		/// Needs to always have at least one element
		/// </summary>
		public List<Theme> Themes = new List<Theme>()
		{
			{ new Theme(ThemeManager.Theme.NewID(), "Blank", Color.white) }
		};

		/// <summary>
		/// Returns a list of all the theme's names
		/// </summary>
		public List<string> ThemeNames
		{
			get
			{
				List<string> names = new List<string>();
				foreach (Theme t in Themes)
					names.Add(t.Name);
				return names;
			}
		}
		#endregion

		#region Private Functions
		void Update()
		{
			if (ApplyInUpdate)
				ApplyThemes();
		}
		#endregion

		#region Public Functions
		public bool IsValidID(string id)
		{
			return Themes.Find(t => t.ID == id) != null;
		}

		/// <summary>
		/// This will propagate any modifications
		/// </summary>
		public void ApplyThemes()
		{
			if (OnApplyThemes != null)
				OnApplyThemes();
		}
		#endregion
	}
}