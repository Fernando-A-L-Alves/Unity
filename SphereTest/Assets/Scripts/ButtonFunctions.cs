using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartCurrentScene()
    {
        //load the currently loaded level.        
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
