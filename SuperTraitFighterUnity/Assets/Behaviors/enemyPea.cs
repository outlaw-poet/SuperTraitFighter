using UnityEngine;
using System.Collections;

public class enemyPea : MonoBehaviour {


public float Gravity = 21f; //downward force
public float TerminalVelocity = 20f; //max downward speed
public float JumpSpeed = 50f;
public float MoveSpeed = 10f;

public Vector3 MoveVector {get; set;}
public float VerticalVelocity {get; set;}
public ArrayList peaClones = new ArrayList();
	
public CharacterController CharacterController;
public GameObject prefabEnemyPeaClone; // drag/drop a reference to the object.
int randStep;
	// Use this for initialization
void Awake () {
	randStep = 0;
	int friendlyHealth = 5;
	CharacterController = gameObject.GetComponent("CharacterController") as CharacterController;
	for (int i = 0; i<  friendlyHealth; i++){
		GameObject newThing = (GameObject) GameObject.Instantiate(prefabEnemyPeaClone);
		newThing.transform.parent = gameObject.transform;
		newThing.transform.position = new Vector3(gameObject.transform.position.x + (i*6),gameObject.transform.position.y,gameObject.transform.position.z);
		//newThing.transform.position.x += (i * 1);	
		peaClones.Add(newThing);
	}
}

// Update is called once per frame
void Update () {
		randStep++;		
		if (randStep > 50){
			randStep = 0;
			int x = 0;
			int y = 0;
			int z = 0;			
			foreach (GameObject clone in peaClones){
				switch (Random.Range(1,6)){
				case 1:
					x = y = z = 0;
					break;
				case 2:
					//x += 1;
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
	VerticalVelocity = MoveVector.y;
	MoveVector = Vector3.zero;
	MoveVector += new Vector3(-1,0,0);
}

void HandleActionInput(){
	if(Random.Range(1,20) == 1){
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

