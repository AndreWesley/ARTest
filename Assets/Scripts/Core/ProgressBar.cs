using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

#pragma warning disable 0649
	[SerializeField] private Image bar;
	[SerializeField] private Text progressText;
	[SerializeField] private ScriptableFloat progress;
#pragma warning restore 0649
	private Coroutine updateProgress;

	void OnDestroy() {
		ResetProgress();	
	}

	private IEnumerator UpdateProgress () {
		ResetProgress ();

		while (bar.fillAmount != 1f) {
			bar.fillAmount = progress.value;
			progressText.text = progress.value * 100f + " %";
			yield return null;
		}
	}

	private void ResetProgress () {
		progress.value = 0f;
		bar.fillAmount = progress.value;
		progressText.text = progress.value + " %";
	}

	public void StartProgress () {
		updateProgress = StartCoroutine (UpdateProgress ());
	}

	public void Abort () {
		StopCoroutine (updateProgress);
		ResetProgress ();
	}
}