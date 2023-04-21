using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void LoadingNewScene()
	{
		SceneManager.LoadScene ("Main"); //Main 씬으로 이동
		Debug.Log("씬이동~");
	}

    public void LoadingUndoScene()
    {
        SceneManager.LoadScene("0421"); //Main 씬으로 이동
        Debug.Log("씬이동~");
    }

}
