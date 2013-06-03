using UnityEngine;
using System.Collections;

public class SectorToScenePosition {
	
	public Vector3 GetScenePosition(Ship ship, Player player) {
		//get difference between player sector and ship
		int sectorOffsetX = ship.sector.x - player.sector.x;
		int sectorOffsetY = ship.sector.y - player.sector.y;
		int sectorOffsetZ = ship.sector.z - player.sector.z;
		
		//get difference between player and ship sectorPosition
		float positionOffsetX = ship.sectorPosition.x - player.sectorPosition.x;
		float positionOffsetY = ship.sectorPosition.y - player.sectorPosition.y;
		float positionOffsetZ = ship.sectorPosition.z - player.sectorPosition.z;
		
		//get final distance between them by allowing for crossing of sector limit
		positionOffsetX = positionOffsetX + sectorOffsetX * player.handlerScene.sectorSize;
		positionOffsetY = positionOffsetY + sectorOffsetY * player.handlerScene.sectorSize;
		positionOffsetZ = positionOffsetZ + sectorOffsetZ * player.handlerScene.sectorSize;
		
		return new Vector3(positionOffsetX, positionOffsetY, positionOffsetZ);
	}
		
	public Vector3 GetScenePosition(Sector sector, SectorPosition position, Player player) {
		//get difference between player sector and ship
		int sectorOffsetX = sector.x - player.sector.x;
		int sectorOffsetY = sector.y - player.sector.y;
		int sectorOffsetZ = sector.z - player.sector.z;
		
		//get difference between player and ship sectorPosition
		float positionOffsetX = position.x - player.sectorPosition.x;
		float positionOffsetY = position.y - player.sectorPosition.y;
		float positionOffsetZ = position.z - player.sectorPosition.z;
		
		//get final distance between them by allowing for crossing of sector limit
		positionOffsetX = positionOffsetX + sectorOffsetX * player.handlerScene.sectorSize;
		positionOffsetY = positionOffsetY + sectorOffsetY * player.handlerScene.sectorSize;
		positionOffsetZ = positionOffsetZ + sectorOffsetZ * player.handlerScene.sectorSize;
		
		return new Vector3(positionOffsetX, positionOffsetY, positionOffsetZ);
	}
	
}
