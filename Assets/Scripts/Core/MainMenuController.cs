using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private GameObject downloadScreen;
	[SerializeField] private ContentDownloader downloader;
	[SerializeField] private ProgressBar progressBar;
#pragma warning restore 0649
    
	public void BusinessCard() {
		bool activeDownloadScreen = true;
		if (PlayerPrefs.HasKey(Constants.FILE_PATH_KEY)) {
			string filePath = PlayerPrefs.GetString(Constants.FILE_PATH_KEY);
			if (File.Exists(filePath)) {
				activeDownloadScreen = false;
			}
		}
		if (activeDownloadScreen) {
			downloadScreen.SetActive(activeDownloadScreen);
			progressBar.StartProgress();
		}
	}

	public void QuitButton() {
		Application.Quit();
	}
}
