using System;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
	[Serializable]
	public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		#region Fields

		[SerializeField] private List<SerializedDictionaryValue<TKey, TValue>> values = new();

		private bool isDeserializeNow;

		#endregion

		#region Constructors
	
		public SerializedDictionary() : base() {}

		public SerializedDictionary(IDictionary<TKey, TValue> dictionary) 
			: base(dictionary) => BakeValues();
	
		public SerializedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) 
			: base(dictionary,comparer) => BakeValues();

		public SerializedDictionary(int capacity) : base(capacity) => BakeValues();
	
		public SerializedDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) =>
			BakeValues();
	
		#endregion

		#region ISerializationCallbackReceiver

		public void OnBeforeSerialize()
		{
			if (isDeserializeNow) return;
			BakeValues();
		}

		public void OnAfterDeserialize()
		{
			isDeserializeNow = true;

			Clear();

			foreach (var item in values)
			{
				try
				{
					Add(item.key, item.value);
				}
				catch (ArgumentException argumentException)
				{
					Add(default, item.value);
				}
			}
		
			isDeserializeNow = false;
		}

		#endregion

		private void BakeValues()
		{
			values.Clear();
			values.Capacity = Count;
			foreach (var item in this)
			{
				values.Add(new SerializedDictionaryValue<TKey, TValue>(item.Key, item.Value));
			}
		}
	}
}