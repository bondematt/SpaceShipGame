using UnityEngine;
using System.Collections;

public static class CreateTileMesh {
	static public MeshAttributes createTileVerticesTriangles (Vector3 tilePosition, Vector3 tileDimensions){
		Vector3 p0;
		Vector3 p1;
		Vector3 p2;
		Vector3 p3;
		Vector3 p4;
		Vector3 p5;
		Vector3 p6;
		Vector3 p7;
		
		MeshAttributes meshAttributes = new MeshAttributes();
		meshAttributes.vertices = new Vector3[24];
		meshAttributes.triangles = new int[36];
		
		//top vertices
		p0 = (new Vector3(tileDimensions.x,tileDimensions.y,tileDimensions.z) + tilePosition); //front right
		p1 = (new Vector3(tileDimensions.x,tileDimensions.y,-tileDimensions.z) + tilePosition); // back right
		p2 = (new Vector3(-tileDimensions.x,tileDimensions.y,tileDimensions.z) + tilePosition); //front left
		p3 = (new Vector3(-tileDimensions.x,tileDimensions.y,-tileDimensions.z) + tilePosition); //back left
		
		//bottom vertices
		p4 = (new Vector3(tileDimensions.x,-tileDimensions.y,tileDimensions.z) + tilePosition); //front right
		p5 = (new Vector3(tileDimensions.x,-tileDimensions.y,-tileDimensions.z) + tilePosition); // back right
		p6 = (new Vector3(-tileDimensions.x,-tileDimensions.y,tileDimensions.z) + tilePosition); //front left
		p7 = (new Vector3(-tileDimensions.x,-tileDimensions.y,-tileDimensions.z) + tilePosition); //back left
		
		//top face
		meshAttributes.vertices[0] = p2;
		meshAttributes.vertices[1] = p0;
		meshAttributes.vertices[2] = p1;
		meshAttributes.vertices[3] = p3;
		//forward face
		meshAttributes.vertices[4] = p0;
		meshAttributes.vertices[5] = p2;
		meshAttributes.vertices[6] = p6;
		meshAttributes.vertices[7] = p4;
		//right face
		meshAttributes.vertices[8] = p1;
		meshAttributes.vertices[9] = p0;
		meshAttributes.vertices[10] = p4;
		meshAttributes.vertices[11] = p5;
		//back face
		meshAttributes.vertices[12] = p3;
		meshAttributes.vertices[13] = p1;
		meshAttributes.vertices[14] = p5;
		meshAttributes.vertices[15] = p7;
		//left face
		meshAttributes.vertices[16] = p2;
		meshAttributes.vertices[17] = p3;
		meshAttributes.vertices[18] = p7;
		meshAttributes.vertices[19] = p6;
		//bottom face
		meshAttributes.vertices[20] = p4;
		meshAttributes.vertices[21] = p6;
		meshAttributes.vertices[22] = p7;
		meshAttributes.vertices[23] = p5;
		
		//THESE ARE THE TRIS, NOT THE FACES!!!
		//top tris
		//203
		meshAttributes.triangles[0] = 0;
		meshAttributes.triangles[1] = 1;
		meshAttributes.triangles[2] = 3;
		//013
		meshAttributes.triangles[3] = 1;
		meshAttributes.triangles[4] = 2;
		meshAttributes.triangles[5] = 3;
		
		//front tris
		//026
		meshAttributes.triangles[6] = 4;
		meshAttributes.triangles[7] = 5;
		meshAttributes.triangles[8] = 6;
		//064
		meshAttributes.triangles[9] = 4;
		meshAttributes.triangles[10] = 6;
		meshAttributes.triangles[11] = 7;
		
		//right tris
		//104
		meshAttributes.triangles[12] = 8;
		meshAttributes.triangles[13] = 9;
		meshAttributes.triangles[14] = 10;
		//145
		meshAttributes.triangles[15] = 8;
		meshAttributes.triangles[16] = 10;
		meshAttributes.triangles[17] = 11;
		
		//back tris
		//315
		meshAttributes.triangles[18] = 12;
		meshAttributes.triangles[19] = 13;
		meshAttributes.triangles[20] = 14;
		//357
		meshAttributes.triangles[21] = 12;
		meshAttributes.triangles[22] = 14;
		meshAttributes.triangles[23] = 15;
		
		//left tris
		//237
		meshAttributes.triangles[24] = 16;
		meshAttributes.triangles[25] = 17;
		meshAttributes.triangles[26] = 18;
		//276
		meshAttributes.triangles[27] = 16;
		meshAttributes.triangles[28] = 18;
		meshAttributes.triangles[29] = 19;
		
		//bottom tris
		//467
		meshAttributes.triangles[30] = 20;
		meshAttributes.triangles[31] = 21;
		meshAttributes.triangles[32] = 22;
		//475
		meshAttributes.triangles[33] = 20;
		meshAttributes.triangles[34] = 22;
		meshAttributes.triangles[35] = 23;
		
		return meshAttributes;
	}
}
