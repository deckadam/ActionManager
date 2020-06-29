using System.Collections.Generic;
using DeckAdam.ActionManager.SettingsContent;
using DeckAdam.ActionManager.UIComponent;
using DeckAdam.ActionManager.UIComponent.IdentifierCountent;
using DeckAdam.ActionManager.UIComponent.LogContent;
using UnityEditor;

namespace DeckAdam.ActionManager
{
	internal class EditoreCore : EditorWindow
	{
		internal static EditoreCore Instance;
		private ToolBar _actionToolBar;
		private List<Content> _contents;
		private int _tabLayout;

		[MenuItem("ActionManager/Debug options")]
		private static void DebugOptions()
		{
			Instance = (EditoreCore) GetWindow(typeof(EditoreCore), false, Constants.Title);
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
			_contents = new List<Content>()
			{
				new LogContent(),
				new IdentifierContent(),
				new SettingsContent.SettingsContent()
			};

			_actionToolBar = new ToolBar(new string[]
			{
				_contents[0].ContentName,
				_contents[1].ContentName,
				_contents[2].ContentName
			});
		}
	}
}