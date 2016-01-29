using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	private const float Speed = 1.2f;
	public int elementType {get; private set;}

	void Start() {
		
	}
	void Update() {
		// 左に移動
		Vector3 position = transform.position;
		position.x -= Speed * Time.deltaTime;
		transform.position = position;
	}
	public void Init(int elementType, Vector3 position) {
		this.elementType = elementType;
		Color colorOffset = new Color(0.2f, 0.2f, 0.2f);
		if (elementType == 0) {
			this.GetComponent<SpriteRenderer>().color = Color.red + colorOffset;
		} else if (elementType == 1) {
			this.GetComponent<SpriteRenderer>().color = Color.yellow + colorOffset;
		} else if (elementType == 2) {
			this.GetComponent<SpriteRenderer>().color = Color.blue + colorOffset;
		}
		transform.position = position;
		gameObject.SetActive(true);
	}
	public void Term() {
		gameObject.SetActive(false);
	}
	public bool IsLiving() {
		return transform.position.x >= GameManager.Screen.xMin;
	}
}
