using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour {
	
	TileMap _tileMap;
	
	Vector3 currentTileCoord; //	the location of cube in TileSelectionIndicator must be half of the size of itself
	
	public Transform selectionCube;
	
	void Start() {
		_tileMap = GetComponent<TileMap>();
	}

	// Update is called once per frame
	void Update () {
		
		Ray ray;

		#if UNITY_ANDROID

		if (Input.touchCount > 0) {
			ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			SelectionCubeMover(ray);
		}

		#else

		if(Input.GetButton("Fire1")){
			ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			SelectionCubeMover(ray);
		}

		#endif
	}

	void SelectionCubeMover(Ray ray){
		RaycastHit hitInfo;

		if( GetComponent<Collider>().Raycast( ray, out hitInfo, Mathf.Infinity ) ) {
			int x = Mathf.FloorToInt( hitInfo.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt( hitInfo.point.z / _tileMap.tileSize);
			//Debug.Log ("Tile: " + x + ", " + z);

			currentTileCoord.x = x;
			currentTileCoord.z = z;

			selectionCube.transform.position = currentTileCoord * _tileMap.tileSize;//*5f;
			Debug.Log (selectionCube.transform.position);
		}
		else {
			// Hide selection cube?
		}

		if(Input.GetMouseButtonDown(0)) {
			Debug.Log ("Click!");
		}
	}
}
