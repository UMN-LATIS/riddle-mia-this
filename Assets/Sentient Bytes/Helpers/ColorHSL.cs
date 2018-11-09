using UnityEngine;
using System;
using System.Collections;

namespace SentientBytes.Helpers
{
	/// <summary>
	/// HSL format, convenience class intended to mimic Unity's Color class
	/// </summary>
	public struct ColorHSL
	{
		#region Public Vars
		public float h, s, l, a;
		#endregion

		#region Public Functions
		public ColorHSL(float h, float s, float l, float a = 1f)
		{
			this.h = h;
			this.s = s;
			this.l = l;
			this.a = a;
		}

		public ColorHSL(Color color)
		{
			h = s = l = 0f;
			a = color.a;

			float max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
			float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));

			l = (max + min) / 2f;

			if (min != max)
			{
				float delta = max - min;
				s = l > 0.5f ? delta / (2f - max - min) : delta / (min + max);

				if (max == color.r)
					h = (color.g - color.b) / delta + (color.g < color.b ? 6f : 0f);
				else if (max == color.g)
					h = (color.b - color.r) / delta + 2f;
				else if (max == color.b)
					h = (color.r - color.g) / delta + 4f;

				h /= 6f;
			}
		}

		public static implicit operator ColorHSL(Color src)
		{
			return FromColor(src);
		}

		public static implicit operator Color(ColorHSL src)
		{
			return ToColor(src);
		}

		public static ColorHSL FromColor(Color color)
		{
			return new ColorHSL(color);
		}

		public static Color ToColor(ColorHSL color)
		{
			float
				r = color.l,
				g = color.l,
				b = color.l,
				a = color.a;

			if (color.l <= 0f)
				color.l = 0.001f;
			if (color.l >= 1f)
				color.l = 0.999f;

			if (color.s != 0f)
			{
				var m2 = color.l < 0.5f ? color.l * (color.s + 1f) : color.l + color.s - color.l * color.s;
				var m1 = color.l * 2f - m2;
				r = ExtractRGB(m1, m2, color.h + 1f / 3f);
				g = ExtractRGB(m1, m2, color.h);
				b = ExtractRGB(m1, m2, color.h - 1f / 3f);
			}

			return new Color(r, g, b, a);
		}
		#endregion

		#region Private Functions
		private static float ExtractRGB(float m1, float m2, float hue)
		{
			if (hue < 0f)
				hue += 1f;
			if (hue > 1f)
				hue -= 1f;
			if (hue * 6f < 1f)
				return m1 + (m2 - m1) * hue * 6f;
			if (hue * 2f < 1f)
				return m2;
			if (hue * 3f < 2f)
				return m1 + (m2 - m1) * (2f / 3f - hue) * 6f;
			return m1;
		}
		#endregion
	}
}