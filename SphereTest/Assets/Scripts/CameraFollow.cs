using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject PlayerGameObject;//Set by dragging the player from scene to the scripts    

    private Vector3 _offset;
    // Use this for initialization
    void Start () {
        _offset = transform.position - PlayerGameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {        
        transform.position = PlayerGameObject.transform.position + _offset;
    }
}
