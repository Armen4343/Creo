using System;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
	public static class SL
	{
		private static readonly Dictionary<Type, IService> services = new(30);

		#region Methods

		public static void AddSingle<TService>(TService instance, SetMode setMode = SetMode.None)
			where TService : class, IService
		{
			if (instance == null) return;
			if (setMode == SetMode.Force)
			{
				services[typeof(TService)] = instance;
				return;
			}

			if (services.ContainsKey(typeof(TService)))
			{
#if UNITY_EDITOR
				Debug.LogWarning($"Object of type {typeof(TService)} is already exists  >>> USE Force Mode");
#endif
				return;
			}

			services.Add(typeof(TService), instance);
		}

		public static bool GetSingle<TService>(out TService service)
			where TService : class, IService
		{
			if (!services.TryGetValue(typeof(TService), out var value))
			{
#if UNITY_EDITOR
				Debug.LogWarning($"Object of type {typeof(TService)} was not found in the Service Locator.");
#endif
				service = null;
				return false;
			}

			service = value as TService;
			return true;
		}

		public static void Remove<TService>()
			where TService : class, IService
		{
			if (services.ContainsKey(typeof(TService)))
				services.Remove(typeof(TService));
			else
#if UNITY_EDITOR
				Debug.LogWarning($"Object of type {typeof(TService)} was not found in the Service Locator,but your trying to remove it");
#endif
			
		}

		#endregion
	}
}