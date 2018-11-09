using UnityEngine;
using System.Collections.Generic;
using System;

namespace SentientBytes.Helpers
{
	public class MonoBehaviourPlus : MonoBehaviour
	{
		#region Nested Types
		#endregion

		#region Public Vars
		#endregion

		#region Private Vars
		Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();
		#endregion

		#region Public Functions
		/// <summary>
		/// Gets and caches a component. Subsequent requests for the same component will return the cached value
		/// </summary>
		public T Compo<T>() where T : Component
		{
			Type type = typeof(T);
			if (!cachedComponents.ContainsKey(type))
			{
				T t = GetComponent<T>();
				if (t != null)
					cachedComponents[type] = t;
				return t;
			}
			else
				return cachedComponents[type] as T;
		}
		#endregion

		#region Private Functions
		#endregion
	}
}