using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppEvent", menuName = "Realidade Aumentada/AppEvent", order = 0)]
public class ScriptableAppEvent : ScriptableObject {
	private List<IAppEvent> listeners = new List<IAppEvent>();

	public void AddListener(IAppEvent listener) {
		listeners.Add(listener);
	}

	public void RemoveListener(IAppEvent listener) {
		listeners.Remove(listener);
	}

	public void Call(){
		for (int i = 0; i < listeners.Count; i++)
		{
			listeners[i].OnEventCalled();
			Debug.Log("call");
		}
	}
}