var target : Transform;
var damping = 6.0;
var smooth = true;

@script AddComponentMenu("Camera-Control/Smooth Look At")

function LateUpdate () {
	if (target) {
		if (smooth)
		{
			if (transform.position.z > target.position.z + 1){
				transform.position.z -= 0.1;
			}
			else if (transform.position.z < target.position.z -1){
				transform.position.z += 0.1;
			}
		}
		else
		{
			// Just lookat
		    transform.LookAt(target);
		}
	}
}

function Start () {
	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
}