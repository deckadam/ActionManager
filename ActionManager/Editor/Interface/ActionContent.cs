using UnityEditor;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionContent
	{
		internal virtual string ContentName => "Default name";

		internal virtual void Display(EditorWindow editor)
		{
		}

		internal virtual void Refresh()
		{
		}
	}
}