using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour {

    LineRenderer _lineForShot;

    Ray _camRay;
    RaycastHit _shootHit;
    float _range = 100;
    int _shootableMask;

    float _timer = 0;
    float _shotDisplayTime = 0.05f;
    // Use this for initialization
    void Start () {
        _shootableMask = LayerMask.GetMask("TargettableLayer");
        _lineForShot = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        _timer += Time.deltaTime;

        //This is to determine if the mouse click is or isn't on the "restart button"
        //Not sure if it's the best way, but so far it worked: "False" on button, "True" at the rest
        var isClickOnGameObject = EventSystem.current.currentSelectedGameObject == null;
        //print("isClickOnGameObject: " + isClickOnGameObject);

        var leftmouse = Input.GetButton("Fire1");
        var rightmouse = Input.GetButton("Fire2");

        if (isClickOnGameObject && leftmouse || rightmouse)//player clicked mouse
        {   
            _timer = 0f;

            // Create a ray from the mouse cursor on screen in the direction of the camera.
            _camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _lineForShot.enabled = true;
            _lineForShot.SetPosition(0, transform.position);//start of line = ball

            //print("start = " + lineForShot.GetPosition(0));

            if (Physics.Raycast(_camRay, out _shootHit, _range, _shootableMask))
            {
                _lineForShot.SetPosition(1, _shootHit.point);//end of line = where i clicked
                //print("end = " + lineForShot.GetPosition(1));

                var target = _shootHit.collider.GetComponent<TargetBehavior>();
                if (target != null)
                {
                    // the enemy reaction                    
                    if (leftmouse)
                        target.ChangeColor();
                    else
                        target.Disable();
                }
            }
            else
            {
                _lineForShot.SetPosition(1, transform.position + _camRay.direction * _range);
            }            
        }

        if(_timer > _shotDisplayTime)
            _lineForShot.enabled = false;
    }

    //Physics update
    void FixedUpdate()
    {        
    }
}
