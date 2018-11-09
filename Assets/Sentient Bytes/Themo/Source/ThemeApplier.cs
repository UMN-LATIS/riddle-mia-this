using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System;
using SentientBytes.Helpers;

namespace SentientBytes.Themo
{
	/// <summary>
	/// Applies a Theme to its listeners
	/// </summary>
	[AddComponentMenu("Sentient Bytes/Themo/Theme Applier"), ExecuteInEditMode]
	public class ThemeApplier : MonoBehaviour
	{
		#region Nested Types
		[Serializable]
		public class ApplierEvent : UnityEvent<Color> { }
		#endregion

		#region Public Vars
		/// <summary>
		/// Theme ID
		/// </summary>
		public string ID;

		/// <summary>
		/// Whether modifications should be applied in the Update loop
		/// </summary>
		public bool ApplyInUpdate = false;

		/// <summary>
		/// Use custom alpha when active
		/// </summary>
		public bool CustomAlpha = false;

		/// <summary>
		/// Use custom lightness
		/// </summary>
		public bool CustomLightness = false;

		/// <summary>
		/// Use custom saturation
		/// </summary>
		public bool CustomSaturation = false;

		/// <summary>
		/// Use a custom hue
		/// </summary>
		public bool CustomHue = false;

		/// <summary>
		/// Whether hue is absolute, or relative to base hue
		/// </summary>
		public bool HueIsAbsolute = false;

		/// <summary>
		/// The custom alpha used, if CustomAlpha is true
		/// </summary>
		[Range(0f, 1f)]
		public float Alpha = 1f;

		/// <summary>
		/// The custom brightness used, if CustomBrightness is true
		/// </summary>
		[Range(0f, 1f)]
		public float Lightness = 0.5f;

		/// <summary>
		/// The custom saturation used, if CustomSaturation is true
		/// </summary>
		[Range(0f, 1f)]
		public float Saturation = 0.5f;

		/// <summary>
		/// The custom hue used, if CustomHue is true
		/// </summary>
		[Range(0f, 1f)]
		public float Hue = 0f;

		/// <summary>
		/// You can subscribe anything that takes a Color argument to this UnityEvent.
		/// Will be triggered everytime ApplyTheme() is called
		/// </summary>
		public ApplierEvent OnApplyTheme = new ApplierEvent();

		/// <summary>
		/// The unmodified color corresponding to our Index
		/// </summary>
		public Color BaseColor
		{
			get { return themeManager.Themes[index].Color; }
		}

		/// <summary>
		/// Current calculated color, i.e. final output color
		/// </summary>
		public Color CalculatedColor
		{
			get
			{
				ColorHSL hsl = BaseColor;
				float a = BaseColor.a;

				if (CustomLightness)
					hsl.l = Lightness;

				if (CustomSaturation)
					hsl.s = Saturation;

				if (CustomHue)
					hsl.h = HueIsAbsolute ? Hue : (hsl.h + Hue) % 1f;

				Color res = hsl;
				res.a = CustomAlpha ? Alpha : a;
				return res;
			}
		}
		#endregion

		#region Private Vars
		ThemeManager themeManager
		{
			get { return ThemeManager.Instance; }
		}

		// Theme index that corresponds to our ID
		int index
		{
			get { return themeManager.Themes.FindIndex(t => t.ID == ID); }
		}
		#endregion

		#region Private Functions
		void Reset()
		{
			if (themeManager)
				ID = themeManager.Themes[0].ID;
			else
				ID = "[NO ID]";
		}

		void Awake()
		{
			if (themeManager)
			{
				themeManager.OnApplyThemes += ApplyTheme;
				ApplyTheme();
			}
			else
			{
				Debug.LogError("Theme Applier needs a Theme Manager to be present in the scene.");
				enabled = false;
			}
		}

		void Update()
		{
			if (!Application.isPlaying || ApplyInUpdate)
				ApplyTheme();
		}

		void OnEnable()
		{
			if (!Application.isPlaying)
				ApplyTheme();
		}
		#endregion

		#region Public Functions
		/// <summary>
		/// Updates the colors of all listeners
		/// </summary>
		public void ApplyTheme()
		{
			if (themeManager && themeManager.IsValidID(ID))
				OnApplyTheme.Invoke(CalculatedColor);
		}
		#endregion
	}
}