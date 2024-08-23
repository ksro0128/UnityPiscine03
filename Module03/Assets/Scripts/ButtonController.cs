using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	public void ClickStartButton()
	{
		SceneManager.Instance.LoadScene("Map1");
		GameManager.instance.SetStage(0);
		GameManager.instance.ResetTotalScore();
	}

    public void ClickExitButton()
	{
		GameManager.instance.ExitGame();
	}
}
