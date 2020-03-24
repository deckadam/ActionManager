using System.Collections;
using DeckAdam.ActionManager;
using UnityEngine;

public class ActionManagerExample : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		//Subscribe to ActionManagerTestEvent with desired Action<> 
		ActionManager.AddAction(TestEvents.ActionManagerTestEvent, () => Debug.Log("Test action triggered"));

		// Start timed event for triggering event
		StartCoroutine(TestTrigger());
	}

	private IEnumerator TestTrigger()
	{
		// Schedule event to be triggered one second later after start method is called
		yield return new WaitForSeconds(1f);
		Debug.Log("Event triggered");
		// Trigger event to run subscribed methods
		ActionManager.TriggerAction(TestEvents.ActionManagerTestEvent);
	}
}

// Define Globally reachable event id class
public static partial class TestEvents
{
	// Event id holder to be used through all codebase without coupling the listeners to each other
	public static readonly long ActionManagerTestEvent = ActionManager.NextTriggerIndex;
}

// Sample usage of static event class for easier access
public static partial class TestEvents
{
	public static readonly long ActionManagerExampleEvent = ActionManager.NextTriggerIndex;
}