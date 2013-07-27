using UnityEngine;
using System.Collections;

public static class RotateAndPositions {
	public static bool RotateAndPositionFacingForward(Transform attachingObject, GameObject referencePoint, Vector3 colliderHitPosition, Vector3 normalOfSurfaceVector, float moveSpeed, float rotateSpeed) {
		bool keepAttaching = true;
		
		Transform rootParent = attachingObject.root;
		
		Vector3 playerFinalPosition = colliderHitPosition + (normalOfSurfaceVector * .91f); //Get position where we want the player to be

		attachingObject.position = Vector3.MoveTowards(attachingObject.position, playerFinalPosition, moveSpeed * Time.deltaTime);
		
		//set reference object at position of player with the rotation of the normal
		referencePoint.SetActive(true);
		
		referencePoint.transform.parent = rootParent;
		referencePoint.transform.position = attachingObject.position;
		referencePoint.transform.up = normalOfSurfaceVector;
		referencePoint.transform.parent = null;
		
		Vector3 pointDirection = attachingObject.forward + attachingObject.position; //world position of point 1 meter in front of player
		
		Vector3 offsetLocal = referencePoint.transform.InverseTransformPoint(pointDirection); //get point in reference point's local space
		
		offsetLocal.y = 0f; //set height in local space to same as player's
		
		pointDirection = referencePoint.transform.TransformPoint(offsetLocal); //put back into world space
		
		referencePoint.transform.LookAt(pointDirection, referencePoint.transform.up); //have reference point look at pointDirection with the correct up normal
		
		referencePoint.transform.parent = rootParent;
		
		Quaternion finalRotation = referencePoint.transform.localRotation;
		
		attachingObject.localRotation =  Quaternion.RotateTowards(attachingObject.localRotation ,finalRotation, rotateSpeed);
		
		referencePoint.transform.parent = null;

		if (Quaternion.Angle(attachingObject.localRotation, finalRotation) < .01f && (playerFinalPosition - attachingObject.position).magnitude <= 0.001f) {
			keepAttaching = false;
		}
		
		return keepAttaching;
	}
}
