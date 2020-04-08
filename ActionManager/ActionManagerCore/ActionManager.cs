using System;
using System.Collections.Generic;
using DeckAdam.ActionManager;
using JetBrains.Annotations;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	public static class ActionManager
	{
		internal static float InitializationTime;

		// Dictionary for holding all events in order
		private static Dictionary<long, EventHolder> _eventListeners;
		internal static Type EventClass;

		// Index holder for trigger parameter
		private static long _lastEventId = long.MinValue;

		/// <summary>
		///     Getter for assigning unique event identifiers
		/// </summary>
		public static long NextTriggerId => ++_lastEventId;

		/// <summary>
		///     Call the Init function on scene load initializing the action manager for avoiding the destroyed listeners
		///     from old scenes to avoid null reference calls
		/// </summary>
		/// <param name="eventClass">
		///		Pass the typeof your event id holder class for tracking
		/// </param>
		public static void Init(Type eventClass)
		{
			_eventListeners = new Dictionary<long, EventHolder>();
			InitializationTime = Time.time;
			EventClass = eventClass;
			ActionManagerDebugger.OnActionManagerInitialized();
		}


		/// <summary>
		///     Clear all listeners to reset all event listeners
		/// </summary>
		public static void ClearListeners()
		{
			_eventListeners.Clear();
			ActionManagerDebugger.OnClearListeners();
		}

		/// <summary>
		///     Remove a specific listener method
		/// </summary>
		/// <param name="id">
		///     Trigger to be removed
		/// </param>
		/// <param name="processToRemove">
		///     Action to be removed
		/// </param>
		public static void RemoveListener(long id, Action processToRemove)
		{
			if (_eventListeners.TryGetValue(id, out var temp))
				temp.RemoveListener(processToRemove);
			ActionManagerDebugger.OnRemoveListener(id, processToRemove.Method.Name);
		}

		/// <summary>
		///     Add a listener to a event id
		/// </summary>
		/// <param name="id">
		///     Event to be subscribed
		/// </param>
		/// <param name="newAction">
		///     Event to be triggered when event is raised
		/// </param>
		public static void AddAction(long id, Action newAction)
		{
			IfKeyExistsDo(id, () => _eventListeners[id].AddListener(newAction), () =>
			{
				_eventListeners[id] = new EventHolder();
				_eventListeners[id].AddListener(newAction);
			});
			ActionManagerDebugger.OnActionAdded(id,newAction.Method.Name);
		}


		/// <summary>
		///     Remove all listeners that listens to a specific event type
		/// </summary>
		/// <param name="id">
		///     Event index to be cleared
		/// </param>
		public static void ClearListener(long id)
		{
			IfKeyExistsDo(id, () => _eventListeners[id].ClearListener());
			ActionManagerDebugger.OnClearListener(id);
		}

		/// <summary>
		///     Raise the events that listen for specified trigger index
		/// </summary>
		/// <param name="id">
		///     Specific index number to be raised
		/// </param>
		public static void TriggerAction(long id)
		{
			IfKeyExistsDo(id, () => _eventListeners[id].ProcessDelegates());
			ActionManagerDebugger.OnTriggerAction(id);
		}

		// Is key has been created or not
		private static bool IsKeyContained(long id)
		{
			return _eventListeners.ContainsKey(id);
		}

		// For checking if the desired key has been assigned or not
		private static void IfKeyExistsDo(long id, Action ifToDo, Action elseToDo = null)
		{
			(IsKeyContained(id) ? ifToDo : elseToDo)?.Invoke();
		}

		#region Nested type: EventHolder

		// Data holder class which works internally
		private class EventHolder
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

		#endregion
	}
}