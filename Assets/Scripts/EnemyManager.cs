using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
	// 生成元のPrefab
	public GameObject prefab;

	private const int MaxBalls = 200;
	private LinkedList<Ball> freeBalls = new LinkedList<Ball>();
	private LinkedList<Ball> activeBalls = new LinkedList<Ball>();
	
	void Start() {
		// 初期化時にBallをプールする
		for (int i = 0; i < MaxBalls; i++) {
			var ballobj = Instantiate<GameObject>(prefab);
			var ball = ballobj.GetComponent<Ball>();
			ball.transform.parent = this.transform;
			ball.gameObject.SetActive(false);
			this.freeBalls.AddLast(ball);
		}
	}
	void Update() {
		// オブジェクトの生存チェック
		for (var node = this.activeBalls.First; node != null; ) {
			var ball = node.Value;
			if (!ball.IsLiving()) {
				ball.Term();
				var next = node.Next;
				this.activeBalls.Remove(node);
				this.freeBalls.AddLast(ball);
				node = next;
			} else {
				node = node.Next;
			}
		}

		// 毎フレームにボールを生成する
		this.GenBall();
	}

	// ボール生成
	void GenBall() {
		if (this.freeBalls.Count == 0) {
			// プールに何もないときは終了
			return;
		}

		var newBall = this.freeBalls.First.Value;
		this.freeBalls.RemoveFirst();
		this.activeBalls.AddLast(newBall);

		// 初期化
		newBall.Init((int)(Random.value * 3),
			new Vector3(2.0f, Random.Range(-1.0f, 1.0f)));
	}
	// ボール破壊
	public void DestroyBall(Ball target) {
		var node = this.activeBalls.Find(target);
		if (node != null) {
			// 終了処理
			target.Term();
			this.activeBalls.Remove(node);
			this.freeBalls.AddLast(target);
		}
	}
}
