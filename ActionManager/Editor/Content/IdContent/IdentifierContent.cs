using System.Collections.Generic;
using DeckAdam.ActionManager.Core.Repo;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent.IdentifierCountent
{
	internal class IdentifierContent :  Content
	{
		private ScrollableTextArea _actionScrollableTextArea;
		private VerticalArea _verticalTagArea;
		private List<Button> _idButtons;
		private Button _saveToFileButton;
		private Button _loadFromFileButton;

		internal IdentifierContent()
		{
			_actionScrollableTextArea = new ScrollableTextArea(new GUIStyle(GUI.skin.textArea)
			{
				wordWrap = false,
				stretchWidth = true,
				stretchHeight = true
			});
			GetSelectedIdListeners(long.MinValue);

			var scrollBar = new ScrollBar();
			_verticalTagArea = new VerticalArea(scrollBar.BeginScrollView, scrollBar.EndScrollView);
			_idButtons = new List<Button>();
			Repository.OnIdentifiersUpdated += AdjustTagButtons;

			_saveToFileButton = new Button(Constants.SavetoFile, SaveToFileButton);
			_loadFromFileButton = new Button(Constants.LoadFromFile, LoadFromSaveButton);
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		internal override void Refresh()
		{
			AdjustTagButtons();
		}

		private void DrawTags(EditorWindow editor)
		{
			_verticalTagArea.BeginVerticalArea(0, 30, 200, (int) editor.position.height - 10);
			_saveToFileButton.ProcessButton();
			_loadFromFileButton.ProcessButton();

			foreach (var label in _idButtons)
				label.ProcessButton();

			_verticalTagArea.EndVerticalArea();
		}

		private void DrawTextArea(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(200, 25, editor.position.width - 205, editor.position.height - 30));
			_actionScrollableTextArea.Draw();
			GUILayout.EndArea();
		}

		private void GetSelectedIdListeners(long id)
		{
			_actionScrollableTextArea.ClearContext();

			if (!Repository.IsListenerConnectedWithId(id)) return;

			var connections = Repository.GetConnectedListenersWithId(id);

			foreach (var connection in connections)
				_actionScrollableTextArea.AppendContext(connection);
		}

		private void AdjustTagButtons()
		{
			_idButtons.Clear();

			foreach (var label in Repository.GetIdentifiers().Keys)
			{
				_idButtons.Add(new Button(Repository.GetIdentifierName(label), () => GetSelectedIdListeners(label)));
			}
		}

		private void LoadFromSaveButton()
		{
			Repository.LoadIdentifierStatus();
			AdjustTagButtons();
		}

		private void SaveToFileButton()
		{
			Repository.SaveIdentifierStatus();
		}
	}
}