using UnityEngine;
using System.Collections;

//base class for ship parts
public class Part {
	public int health;
	
	public Part() {
		health = 0;
	}
	
	public Part(byte health) {
		this.health = health;
	}
}
