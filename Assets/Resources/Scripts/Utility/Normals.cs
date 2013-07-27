using UnityEngine;
using System.Collections;

public static class Normals {
	public static Vector3 Normal (RaycastHit hitInfo) {
		MeshCollider meshCollider = (MeshCollider) hitInfo.collider;
		
		Mesh mesh = meshCollider.sharedMesh;
		
		Vector3[] normals = mesh.normals;
		int[] triangles = mesh.triangles;
		
		// Extract local space normals of the triangle we hit 
		Vector3 n0 = normals[triangles[hitInfo.triangleIndex * 3 + 0]]; 
		Vector3 n1 = normals[triangles[hitInfo.triangleIndex * 3 + 1]];
		Vector3 n2 = normals[triangles[hitInfo.triangleIndex * 3 + 2]];
		
		// interpolate using the barycentric coordinate of the hitpoint 
		Vector3 baryCenter = hitInfo.barycentricCoordinate;
		
		// Use barycentric coordinate to interpolate normal 
		Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z; 
		// normalize the interpolated normal 
		interpolatedNormal =  interpolatedNormal.normalized; 
		
		// Transform local space normals to world space 
		interpolatedNormal = hitInfo.collider.transform.TransformDirection(interpolatedNormal); 
		
		return interpolatedNormal;
	}
}
