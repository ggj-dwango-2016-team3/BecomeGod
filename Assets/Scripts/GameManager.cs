using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static readonly Rect Screen = new Rect(-2.0f, -1.0f, 4.0f, 2.0f);
	
	// パワーメーター
	Slider[] uiPowers = new Slider[3];
	
	void Start() {
		this.uiPowers[0] = GameObject.Find("uiPowerX").GetComponent<Slider>();
		this.uiPowers[1] = GameObject.Find("uiPowerY").GetComponent<Slider>();
		this.uiPowers[2] = GameObject.Find("uiPowerZ").GetComponent<Slider>();

		// BGM再生開始
		SoundManager.Instance.PlayMusic(Music.Stage);
	}
	
	void Update() {
	
	}

	public void AddElementPower(int elementType) {
		var bar = this.uiPowers[elementType];
		// 指定されたパワーを5%上げる
		bar.value += 5;

		// それ以外のパワーを2%下げる
		for (int i = 0; i < 3; i++) {
			if (i == elementType) {
				continue;
			}
			this.uiPowers[i].value -= 2;
		}
		// パワー取得効果音を鳴らす
		SoundManager.Instance.PlaySound(Sound.Item);
	}
}
