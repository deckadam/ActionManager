using System;
using System.Collections.Generic;
using DeckAdam.ActionManager.Repo;
using UnityEngine;

namespace DeckAdam.ActionManager.Core
{
	public static class ActionManager
	{
		//TODO: Find out how to make attributes functional to use in debugger invokes
		internal static float InitializationTime;

		//Check for system initialization
		private static bool _isInitialized;

		// Dictionary for holding all events in order
		private static Dictionary<long, Event> _eventListeners;

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
			if (_isInitialized)
			{
				Debugger.OnReinitialization();
				return;
			}
			Repository.InitializeRepository();
			_isInitialized = true;
			_eventListeners = new Dictionary<long, Event>();
			InitializationTime = Time.time;
			EventClass = eventClass;
			Debugger.OnActionManagerInitialized();
		}


		/// <summary>
		///     Clear all listeners to reset all event listeners
		/// </summary>
		public static void ClearListeners()
		{
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}

			_eventListeners.Clear();
			_lastEventId = long.MinValue;
			Debugger.OnClearListeners();
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
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}

			if (_eventListeners.TryGetValue(id, out var temp))
			{
				temp.RemoveListener(processToRemove);
				Debugger.OnRemoveListener(id, processToRemove.Method.Name);
			}
			else
			{
				Debugger.OnInvalidRemoveListener();
			}
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
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}

			if (newAction == null)
			{
				Debugger.OnAddingEmptyAction();
				return;
			}

			IfKeyExistsDo(id, () => _eventListeners[id].AddListener(newAction), () =>
			{
				_eventListeners[id] = new Event();
				_eventListeners[id].AddListener(newAction);
			});
			Debugger.OnActionAdded(id, newAction.Method.Name);
		}


		/// <summary>
		///     Remove all listeners that listens to a specific event type
		/// </summary>
		/// <param name="id">
		///     Event index to be cleared
		/// </param>
		public static void ClearListener(long id)
		{
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}

			IfKeyExistsDo(id,
				() => _eventListeners[id].ClearListener(),
				() => Debugger.OnClearingNonExistingKey());
			Debugger.OnClearListener(id);
		}

		/// <summary>
		///     Raise the events that listen for specified trigger index
		/// </summary>
		/// <param name="id">
		///     Specific index number to be raised
		/// </param>
		public static void TriggerAction(long id)
		{
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}

			IfKeyExistsDo(id,
				() => _eventListeners[id].ProcessDelegates(),
				() => Debugger.OnTriggeringNonExistingEvent());
			Debugger.OnTriggerAction(id);
		}

		/// <summary>
		/// Save the current status of identifiers
		/// </summary>
		public static void SaveIdentifierStatus()
		{
			if (!Application.isEditor)
			{
				return;
			}
			
			if (!_isInitialized)
			{
				Debugger.OnNonInitializedOperation();
				return;
			}
			
			Repository.SaveIdentifierStatus();
		}

		// For checking if the desired key has been assigned or not
		private static void IfKeyExistsDo(long id, Action ifToDo, Action elseToDo = null)
		{
			(_eventListeners.ContainsKey(id) ? ifToDo : elseToDo)?.Invoke();
		}
	}
}