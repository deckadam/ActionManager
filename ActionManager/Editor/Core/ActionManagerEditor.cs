using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	public class ActionManagerEditor : EditorWindow
	{
		internal static ActionManagerEditor Instance;
		
		private List<ActionContent> _contents;
		private int _tabLayout;

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
				"Logs",
				"Id",
				"Settings"
			});
			if (_contents == null) Initialize();
			_contents[_tabLayout].Display();
		}

		internal void Initialize()
		{
			Instance = this;
			_contents = new List<ActionContent>()
			{
				new LogContent(),
				new IdContent(),
				new SettingsContent()
			};
		}
	}
}