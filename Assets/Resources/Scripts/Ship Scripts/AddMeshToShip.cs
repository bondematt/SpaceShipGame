using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class AddMeshToShip {
	
	static public void addMesh (MeshAttributes meshAttributes, Mesh shipMesh) {
				
		List<Vector3> shipVertices = shipMesh.vertices.ToList();
		
		shipVertices.AddRange(meshAttributes.vertices);
		
		for (int i = 0; i < meshAttributes.triangles.Length; i++) {
			meshAttributes.triangles[i] = meshAttributes.triangles[i] + shipMesh.vertexCount;
		}
		
		List<int> shipTris = shipMesh.triangles.ToList();
						
		shipTris.AddRange(meshAttributes.triangles);
		
		shipMesh.Clear();
		
		shipMesh.vertices = shipVertices.ToArray();
		
		shipMesh.triangles = shipTris.ToArray();
	}
	
	static public void addMesh (List<MeshAttributes> meshAttributesList, Mesh shipMesh) {
		
		List<Vector3> shipVertices = new List<Vector3>();
		
		List<int> shipTris = new List<int>();
		
		if (shipMesh.vertices.Count() > 0)
			shipVertices = shipMesh.vertices.ToList();
		if (shipMesh.triangles.Count() > 0)
			shipTris = shipMesh.triangles.ToList();
		
		int lastVertices = shipVertices.Count;
		
		foreach (MeshAttributes meshAttributes in meshAttributesList) {
			
			for (int i = 0; i < meshAttributes.triangles.Length; i++) {
				meshAttributes.triangles[i] = meshAttributes.triangles[i] + shipVertices.Count;
			}
			
			shipVertices.AddRange(meshAttributes.vertices);
			
			lastVertices = meshAttributes.vertices.Count();
			
			shipTris.AddRange(meshAttributes.triangles);	
		}
		
		shipMesh.Clear();
		
		shipMesh.vertices = shipVertices.ToArray();
		
		shipMesh.triangles = shipTris.ToArray();
		
		shipMesh.RecalculateNormals();
	}
}
