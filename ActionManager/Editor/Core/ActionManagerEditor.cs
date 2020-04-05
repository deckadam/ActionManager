using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	public class ActionManagerEditor : EditorWindow
	{
		//TODO: Add identifiers tab
		//TODO: Keep last selections
		//TODO: Make window refresh on game start

		private int _tabLayout;

		private List<IActionContent> _contents;

		internal static ActionManagerEditor Instance;

		[MenuItem("ActionManager/Debug options")]
		private static void DebugOptions()
		{
			Instance = (ActionManagerEditor) GetWindow(typeof(ActionManagerEditor), false, "Action manager debugger");
			Instance.Show();
		}


		internal void RefreshTabs()
		{
			if (_contents == null) Initialize();
			_contents.ForEach((val) => val.Refresh());
		}

		private void OnGUI()
		{
			_tabLayout = GUILayout.Toolbar(_tabLayout, new string[]
			{
				"Logs", "ID"
			});
			if (_contents == null) Initialize();
			_contents[_tabLayout].Display();
		}

		internal void Initialize()
		{
			Debug.Log("Re initialized");
			Instance = this;
			_contents = new List<IActionContent>()
			{
				new LogContent(),
				new IdContent()
			};
		}
	}
}