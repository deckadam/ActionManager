using System.Collections.Generic;
using System;
using JetBrains.Annotations;

namespace DeckAdam.ActionManager
{
	public static class ActionManager
	{
		private static int _lastTriggerIndex = -1;
		public static int NextTriggerIndex => ++_lastTriggerIndex;

		private static Dictionary<int, ListenerObject> _actionListeners = new Dictionary<int, ListenerObject>();

		public static void ClearListeners()
		{
			_actionListeners.Clear();
		}

		private static bool IsKeyContained(int triggerIndex)
		{
			return _actionListeners.ContainsKey(triggerIndex);
		}

		public static void ClearListener(int triggerIndex)
		{
			IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].ClearListener());
		}

		public static void TriggerAction(int triggerIndex)
		{
			IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].ProcessDelegates());
		}

		private static void IfKeyExistsDo(int triggerIndex, Action ifToDo, Action elseToDo = null)
		{
			(IsKeyContained(triggerIndex) ? ifToDo : elseToDo)?.Invoke();
		}

		public static void RemoveListener(int triggerIndex, Action processToRemove)
		{
			if (_actionListeners.TryGetValue(triggerIndex, out var temp))
				temp.RemoveListener(processToRemove);
		}

		public static void AddAction(int triggerIndex, Action newAction)
		{
			IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].AddListener(newAction), () =>
			{
				_actionListeners[triggerIndex] = new ListenerObject();
				_actionListeners[triggerIndex].AddListener(newAction);
			});
		}

		private class ListenerObject
		{
			private Action _thisAction;

			public void ProcessDelegates()
			{
				_thisAction?.Invoke();
			}

			public void AddListener([NotNull] Action newAction)
			{
				_thisAction += newAction;
			}

			public void RemoveListener([NotNull] Action oldAction)
			{
				_thisAction -= oldAction;
			}

			public void ClearListener()
			{
				_thisAction = null;
			}
		}
	}
}