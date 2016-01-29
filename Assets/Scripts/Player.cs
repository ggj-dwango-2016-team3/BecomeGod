using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float speed = 2.0f;
	EnemyManager enemyManager;
	GameManager gameManager;

	void Start() {
		this.enemyManager = GameObject.FindObjectOfType<EnemyManager>();
		this.gameManager = GameObject.FindObjectOfType<GameManager>();

		// (仮)Unityちゃんを走らせる
		this.GetComponent<Animator>().SetFloat("Speed", 1.0f);
	}
	void Update() {
		// 入力移動処理
		Vector3 direction = Vector3.zero;
		if (Input.GetKey(KeyCode.LeftArrow)) {
			direction.x = -1.0f;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			direction.x =  1.0f;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			direction.y = -1.0f;
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			direction.y =  1.0f;
		}
		Vector3 position = transform.position + direction * speed * Time.deltaTime;
		
		// 移動制限
		if (position.x < GameManager.Screen.xMin) {
			position.x = GameManager.Screen.xMin;
		}
		if (position.x > GameManager.Screen.xMax) {
			position.x = GameManager.Screen.xMax;
		}
		if (position.y < GameManager.Screen.yMin) {
			position.y = GameManager.Screen.yMin;
		}
		if (position.y > GameManager.Screen.yMax) {
			position.y = GameManager.Screen.yMax;
		}
		transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D target) {
		var ball = target.GetComponent<Ball>();
		if (ball) {
			gameManager.AddElementPower(ball.elementType);
			// ボールは消す
			this.enemyManager.DestroyBall(ball);
		}
	}
}
