﻿namespace ExBuddy.OrderBotTags.Gather
{
	using Clio.XmlEngine;
	using ExBuddy.Interfaces;
	using System.Collections;
	using System.Collections.Generic;

	[XmlElement("Items")]
	public class NamedItemCollection : IList<IConditionNamedItem>
	{
		public NamedItemCollection()
		{
			Items = new List<IConditionNamedItem>();
		}

		[XmlElement(XmlEngine.GENERIC_BODY)]
		private List<IConditionNamedItem> Items { get; set; }

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion IEnumerable Members

		#region IEnumerable<INamedItem> Members

		public IEnumerator<IConditionNamedItem> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		#endregion IEnumerable<INamedItem> Members

		#region ICollection<INamedItem> Members

		public int Count
		{
			get { return Items.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public void Add(IConditionNamedItem item)
		{
			Items.Add(item);
		}

		public void Clear()
		{
			Items.Clear();
		}

		public bool Contains(IConditionNamedItem item)
		{
			return Items.Contains(item);
		}

		public void CopyTo(IConditionNamedItem[] array, int arrayIndex)
		{
			Items.CopyTo(array, arrayIndex);
		}

		public bool Remove(IConditionNamedItem item)
		{
			return Items.Remove(item);
		}

		#endregion ICollection<INamedItem> Members

		#region IList<INamedItem> Members

		public int IndexOf(IConditionNamedItem item)
		{
			return Items.IndexOf(item);
		}

		public void Insert(int index, IConditionNamedItem item)
		{
			Items.Insert(index, item);
		}

		public IConditionNamedItem this[int index]
		{
			get { return Items[index]; }
			set { Items[index] = value; }
		}

		public void RemoveAt(int index)
		{
			Items.RemoveAt(index);
		}

		#endregion IList<INamedItem> Members
	}
}