using UnityEngine;
using System.Collections;

namespace SentientBytes.Helpers
{
	public class Singleton<T> : MonoBehaviourPlus where T : MonoBehaviourPlus
	{
		#region Public Vars
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					T[] found = FindObjectsOfType<T>();

					if (found.Length != 1)
						Debug.LogError("There can only be one " + typeof(T).Name + " present in the scene.");
					else
						instance = found[0];
				}
				return instance;
			}
		}
		#endregion

		#region Private Vars
		private static T instance;
		#endregion
	}
}