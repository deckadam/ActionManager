using System;
using System.Collections.Generic;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionManagerEditor : EditorWindow
	{
		internal static ActionManagerEditor Instance;
		private List<ActionContent> _contents;
		private bool _isInitialized = false;
		private int _tabLayout;

		[MenuItem("ActionManager/Debug options")]
		private static void DebugOptions()
		{
			Instance = (ActionManagerEditor) GetWindow(typeof(ActionManagerEditor), false, ActionManagerConstants.Title);
			Instance.Show();
		}

		internal void RefreshTabs()
		{
			_contents?.ForEach((val) => val.Refresh());
		}

		private void OnGUI()
		{
			if (!_isInitialized) Initialize();
			_tabLayout = GUILayout.Toolbar(_tabLayout, new string[]
			{
				_contents[0].ContentName,
				_contents[1].ContentName,
				_contents[2].ContentName
			});
			_contents[_tabLayout].Display(this);
		}

		internal void Initialize()
		{
			_isInitialized = true;
			Instance = this;
			_contents = new List<ActionContent>()
			{
				new ActionLogContent(),
				new ActionIdContent(),
				new ActionSettingsContent()
			};
		}
	}
}