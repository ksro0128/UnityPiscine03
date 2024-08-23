using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour
{
	private bool homeButtonPressed = false;
	[SerializeField] private TMPro.TextMeshProUGUI scoreText;
	[SerializeField] private TMPro.TextMeshProUGUI rankText;


	void Start()
	{
		scoreText.text = "" + GameManager.instance.GetTotalScore();
		rankText.text = GameManager.instance.GetTotalRank();
	}

    public void HomeButtonPressed()
	{
		if (homeButtonPressed)
			return;
		homeButtonPressed = true;
		SceneManager.Instance.LoadScene("Menu");
	}
}
