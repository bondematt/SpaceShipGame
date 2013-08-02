using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class testCombine : MonoBehaviour {
	
	int q = 0;
	
	// Use this for initialization
	void Start () {
		Invoke("combineMeshes", 2);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void combineMeshes () {
		Vector3 startPosition = transform.position;
		transform.position = new Vector3(0,0,0);
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			if (meshFilters[i].gameObject.name == "wallTileZ2")
				Destroy (meshFilters[i].gameObject);
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true);
		transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
		transform.gameObject.SetActive(true);
		
		transform.position = startPosition;
	}
}
