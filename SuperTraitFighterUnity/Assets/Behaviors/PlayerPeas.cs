using UnityEngine;
using System.Collections;

public class PlayerPeas : MonoBehaviour {


public float Gravity = 21f; //downward force
public float TerminalVelocity = 20f; //max downward speed
public float JumpSpeed = 50f;
public float MoveSpeed = 10f;

public Vector3 MoveVector {get; set;}
public float VerticalVelocity {get; set;}
public ArrayList peaClones = new ArrayList();
	
public CharacterController CharacterController;
public GameObject prefabPeaClone; // drag/drop a reference to the object.
public GameObject enemy;

int randStep;
	// Use this for initialization
void Awake () {
		won = false;
		lost = false;
	randStep = 0;
	int friendlyHealth = 5;
	CharacterController = gameObject.GetComponent("CharacterController") as CharacterController;
	for (int i = 0; i<  friendlyHealth; i++){
		GameObject newThing = (GameObject) GameObject.Instantiate(prefabPeaClone);
		newThing.transform.parent = gameObject.transform;
		newThing.transform.position = new Vector3(gameObject.transform.position.x + (i*6),gameObject.transform.position.y,gameObject.transform.position.z);
		//newThing.transform.position.x += (i * 1);	
		peaClones.Add(newThing);
	}
}
	public static bool won = false;
void triggerWin(){
		
	}
	
	public static bool lost = false;
	void triggerLost(){
		
	}

// Update is called once per frame
void Update () {
		if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < 5){
			int result = battleGameBehaviors.compareArmyValue(new battleGameBehaviors.ArmyTraits(TitleBehavior.code, battleGameBehaviors.traitFile), new battleGameBehaviors.ArmyTraits("everything", battleGameBehaviors.traitFile));
			if (result > 0){
				triggerWin();
			}
			else{
				triggerLost();
			}
			Application.LoadLevel(0);
		}
		randStep++;		
		if (randStep > 20){
			randStep = 0;
			//reset on x coordinate in case of jitter
			if (gameObject.transform.position.x != 10){
				gameObject.transform.position = new Vector3(10, gameObject.transform.position.y + 1, gameObject.transform.position.z);
			}
			int x = 0;
			int y = 0;
			int z = 0;			
			//let the clones jiggle a bit randomly
			foreach (GameObject clone in peaClones){
				switch (Random.Range(1,6)){
				case 1:
					x = y = z = 0;
					break;
				case 2:
					x += 1;
					break;
				case 3:
					//y += 1;
					break;
				case 4:
					z += 1;
					break;
				case 5:
					x -= 1;
					break;
				case 6:
					z -= 1;
					break;
					
				default:
					break;
				}
				clone.transform.position = new Vector3(clone.transform.position.x + x, clone.transform.position.y + y, clone.transform.position.z + z);
			}
		}
	checkMovement();
	HandleActionInput();
	processMovement();
		
}

void checkMovement(){
//move l/r
var deadZone = 0.1f;
VerticalVelocity = MoveVector.y;
MoveVector = Vector3.zero;
	if(Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone){
	MoveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);
}
//jump

}
	
	 void OnCollisionEnter(Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audio.Play();
        
    }
	
void HandleActionInput(){
	if(Input.GetButton("Jump")){
	jump();
	}
}

void processMovement(){
	//transform moveVector into world-space relative to character rotation
	MoveVector = transform.TransformDirection(MoveVector);

	//normalize moveVector if magnitude > 1
	if(MoveVector.magnitude > 1){
	MoveVector = Vector3.Normalize(MoveVector);
}

//multiply moveVector by moveSpeed
MoveVector *= MoveSpeed;

//reapply vertical velocity to moveVector.y
MoveVector = new Vector3(MoveVector.z, VerticalVelocity, MoveVector.x);

//apply gravity
applyGravity();

//move character in world-space
CharacterController.Move(MoveVector * Time.deltaTime);
}

void applyGravity(){
if(MoveVector.y > -TerminalVelocity){
	MoveVector = new Vector3(MoveVector.x, (MoveVector.y - Gravity * Time.deltaTime), MoveVector.z);
}
if(CharacterController.isGrounded && MoveVector.y < -1){
MoveVector = new Vector3(MoveVector.x, (-1), MoveVector.z);
}
}

public void jump(){
if(CharacterController.isGrounded){
VerticalVelocity = JumpSpeed;
}
}
}

