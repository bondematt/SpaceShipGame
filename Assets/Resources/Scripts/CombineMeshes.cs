using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombineMeshes {
	Vector3 startPosition;
	CombineInstance[] combine = new CombineInstance[2];
	MeshFilter addMesh;
	MeshFilter baseMesh;
	
	//combines single mesh into the base mesh
	public void combineMeshes (GameObject addMeshObject, GameObject baseMeshObject) {
		startPosition = baseMeshObject.transform.position;
		baseMeshObject.transform.position = new Vector3(0,0,0);
		
		addMesh = addMeshObject.GetComponent<MeshFilter>();
		baseMesh = baseMeshObject.GetComponent<MeshFilter>();
		
		
		if (baseMesh == null) {
			baseMesh.mesh = addMesh.mesh;
			baseMeshObject.transform.position = startPosition;
			return;
		}
		
		combine[0].mesh = addMesh.sharedMesh;
		combine[0].transform = addMesh.transform.localToWorldMatrix;

		combine[1].mesh = baseMesh.sharedMesh;
		combine[1].transform = baseMesh.transform.localToWorldMatrix;

		
        baseMeshObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        baseMeshObject.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true);
		baseMeshObject.transform.gameObject.active = true;
		
		baseMeshObject.transform.position = startPosition;
	}
	
	/*
	//combines multiple tile meshes into the ship mesh
	public void combineMeshes (List<GameObject> tileObjects, GameObject shipObject) {
		Vector3 startPosition = shipObject.transform.position;
		shipObject.transform.position = new Vector3(0,0,0);
		List<MeshFilter> meshFilters = new List<MeshFilter>();
		foreach (GameObject tile in tileObjects) {
			meshFilters.Add(tile.GetComponent<MeshFilter>());
		}
		if (shipObject.GetComponent<MeshFilter>().mesh != null) 
			meshFilters.Add(shipObject.GetComponent<MeshFilter>());
        CombineInstance[] combine = new CombineInstance[meshFilters.Count];
        int i = 0;
        while (i < meshFilters.Count) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			
			if (meshFilters[i].gameObject != shipObject.gameObject)
				GameObject.Destroy (meshFilters[i].gameObject);
			i++;
        }
        shipObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        shipObject.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true);
		shipObject.transform.gameObject.active = true;
		
		shipObject.transform.position = startPosition;
	}*/
}
