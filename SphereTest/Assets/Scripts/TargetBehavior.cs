using UnityEngine;

public class TargetBehavior : MonoBehaviour {

    Renderer _renderer;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(30, 15, -15) * Time.deltaTime);
    }

    public void ChangeColor()
    {
        //print("ChangeColor");
        var r = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        _renderer.material.color = new Color(r, g, b);
    }

    public void Disable()
    {
        //print("Disable");
        gameObject.SetActive(false);
    }
}
