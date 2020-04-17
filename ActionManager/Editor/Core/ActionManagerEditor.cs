using System.Collections.Generic;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;

namespace DeckAdam.ActionManager
{
	internal class ActionManagerEditor : EditorWindow
	{
		internal static ActionManagerEditor Instance;
		private ActionToolBar _actionToolBar;
		private List<ActionContent> _contents;
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
			if (_contents == null) Initialize();
			var result = _actionToolBar.DrawToolbar();
			_contents[result].Display(this);
		}

		private void Initialize()
		{
			Instance = this;
			_contents = new List<ActionContent>()
			{
				new ActionLogContent(),
				new ActionIdContent(),
				new ActionSettingsContent()
			};

			_actionToolBar = new ActionToolBar(new string[]
			{
				_contents[0].ContentName,
				_contents[1].ContentName,
				_contents[2].ContentName
			});
		}
	}
}