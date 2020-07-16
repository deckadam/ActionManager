using System;
using JetBrains.Annotations;

namespace DeckAdam.ActionManager.Core
{
	internal class Event
	{
		private Action _thisAction;

		internal void ProcessDelegates()
		{
			_thisAction?.Invoke();
		}

		internal void AddListener([NotNull] Action newAction)
		{
			_thisAction += newAction;
		}

		internal void RemoveListener([NotNull] Action oldAction)
		{
			_thisAction -= oldAction;
		}

		internal void ClearListener()
		{
			_thisAction = null;
		}
	}
}