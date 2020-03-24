using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	// TODO: Add debugging option
	// TODO: Make debugging window for editor
	// TODO: Make debugging mode preprocessor directive based with #define to remove the overhead
	// TODO: Add editor mode debugging for switchable debug mode
	public static class ActionManager
	{
		// Index holder for trigger parameter
		private static long _lastTriggerIndex = long.MinValue;

		// Getter method to be assign a unique value to each listener
		public static long NextTriggerIndex => ++_lastTriggerIndex;

		// Dictionary for holding all events in order
		private static Dictionary<long, ListenerObject> _actionListeners = new Dictionary<long, ListenerObject>();

		/// <summary>
		/// Clear all listeners to reset all event listeners
		/// </summary>
		public static void ClearListeners()
		{
			_actionListeners.Clear();
			_lastTriggerIndex = long.MinValue;
		}

		/// <summary>
		/// Remove a specific listener method 
		/// </summary>
		/// <code>
		///	private void OnDestroy()
		/// {
		///		ActionManager.RemoveListener(SampleEvents.SampleEvent,SampleMethod);
		/// }
		/// </code>>
		/// <param name="triggerIndex">
		/// Trigger to be removed
		/// </param>
		/// <param name="processToRemove">
		/// Action to be removed
		/// </param>
		public static void RemoveListener(long triggerIndex, Action processToRemove)
		{
			if (_actionListeners.TryGetValue(triggerIndex, out var temp))
				temp.RemoveListener(processToRemove);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="triggerIndex">
		/// Event to be subscribed
		/// </param>
		/// <param name="newAction">
		/// Event to be triggered when event is raised
		/// </param>
		public static void AddAction(long triggerIndex, Action newAction)
		{
			IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].AddListener(newAction), () =>
			{
				_actionListeners[triggerIndex] = new ListenerObject();
				_actionListeners[triggerIndex].AddListener(newAction);
			});
		}


		/// <summary>
		/// Remove all listeners that listens to a specific event type
		/// </summary>
		/// <param name="triggerIndex">
		/// Event index to be cleared
		/// </param>
		public static void ClearListener(long triggerIndex) => IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].ClearListener());

		/// <summary>
		/// Raise the events that listen for specified trigger index
		/// </summary>
		/// <param name="triggerIndex">
		/// Specific index number to be raised
		/// </param>
		public static void TriggerAction(long triggerIndex) => IfKeyExistsDo(triggerIndex, () => _actionListeners[triggerIndex].ProcessDelegates());

		// Is key has been created or not
		private static bool IsKeyContained(long triggerIndex) => _actionListeners.ContainsKey(triggerIndex);

		// For checking if the desired key has been assigned or not
		private static void IfKeyExistsDo(long triggerIndex, Action ifToDo, Action elseToDo = null) => (IsKeyContained(triggerIndex) ? ifToDo : elseToDo)?.Invoke();

		// Data holder class which works internally
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