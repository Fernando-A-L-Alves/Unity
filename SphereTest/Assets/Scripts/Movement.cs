using UnityEngine;

public class Movement : MonoBehaviour {

    public float HorizontalMultiplier = 10;
    public float JumpForce = 15;
    public float SmashForce = 45;
    public bool EnableSmashAttack = true;

    SphereCollider _playerCollider;
    Rigidbody _playerRigidBody;
    //CharacterController playerController; /* Another way of manipulating the player: https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483 */

    bool _isSmashing;

    // Use this for initialization
    void Start () {
        _playerCollider = GetComponent<SphereCollider>();
        _playerRigidBody = GetComponent<Rigidbody>();
        //playerController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        TreatGroundMovement();
        TreatJump();
        if(EnableSmashAttack)
            TreatAirSmash();

        TreatFallForever();
    }

    void TreatFallForever()
    {
        if(transform.position.y < -100)//if player is under 100 units below the plane        
            transform.position = new Vector3(0, 1, 0);//put him back to a little above the origin        
    }

    void FixedUpdate()
    {
        /* Altho this is tecnincally physics...i'll put them in Update
           bc i read that: 
           "AddForce calls are instantaneous, therefore it’s not frame dependent. 
            AddForce is a function used to apply a force to Rigidbody. There are 4 different ways to apply a force:
            -Force: continuous and mass dependent
            -Acceleration: continuous and mass independent
            -Impulse: instant and mass dependent
            -VelocityChange: instant and mass independent" */

        //TreatGroundMovement();
        //TreatJump();
        //TreatAirSmash();
    }

    bool IsGrounded()
    {
        float distanceToTheGround = _playerCollider.bounds.extents.y;//Gets player Y (in this case it's 0.5)        
        var isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.05f/*margin*/);
        //print("IsGrounded: " + isGrounded);
        return isGrounded;
    }
    
    void TreatGroundMovement()
    {
        var leftright = Input.GetAxis("Horizontal");//x
        var updown = Input.GetAxis("Vertical");//z
        //var jump = Input.GetAxis("Jump");//y
        _playerRigidBody.AddForce(leftright * HorizontalMultiplier, 0.0f/*jump * jumpForce*/, updown * HorizontalMultiplier);
    }

    void TreatJump()
    {        
        if (IsGrounded()) //is on the ground 
        {
            _isSmashing = false;//on ground, can't be
            if (Input.GetButtonDown("Jump")) //jump pressed
                _playerRigidBody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }  
    }

    void TreatAirSmash()
    {
        if(!IsGrounded() && Input.GetButtonDown("Jump"))//is in the air and jump pressed
        {
            _isSmashing = true;
            _playerRigidBody.AddForce(Vector3.down * SmashForce, ForceMode.VelocityChange);
            //OBS: Gotta change RigidBody 'colision detection' to 'Continuous' or else it might pass thru the floor (bc of the velocity) 
        }
    }

    /* This method is called when the player collides with something.
     * Since it's a "OnTrigger", the Target (Box) collider must have it's
     * "Is Trigger" property as "true".
     * OBS: Making a Collider as a trigger disables it's physics reactions.
     *      Eg: The player will no longer "bounce" off the target 
     *      (if that's desired, another collider must be made...a non-trigger one)
     */
    void OnTriggerEnter(Collider other)
    {        
        //if i'm using my smash attack and the "thing" i'm colliding with is the "Target"
        if (_isSmashing && other.gameObject.CompareTag("Target"))        
            other.gameObject.SetActive(false);        
    }
}
