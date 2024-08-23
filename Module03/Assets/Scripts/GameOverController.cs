using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private bool homeButtonPressed = false;
	private bool restartButtonPressed = false;


	public void HomeButtonPressed()
	{
		if (homeButtonPressed)
			return;
		homeButtonPressed = true;
		SceneManager.Instance.LoadScene("Menu");
	}

	public void RestartButtonPressed()
	{
		if (restartButtonPressed)
			return;
		restartButtonPressed = true;
		GameManager.instance.RestartGame();
	}
}
