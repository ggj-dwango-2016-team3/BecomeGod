using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	// シングルトンで管理
	public static SoundManager Instance;

	// SEデータClip
	AudioClip[] soundAudioClips = new AudioClip[(int)Sound.Count];
	// BGMデータClip
	AudioClip[] musicAudioClips = new AudioClip[(int)Music.Count];
	
	// 最大SE再生本数
	const int MaxSounds = 32;
	List<AudioSource> soundAudioList = new List<AudioSource>();
	// BGM再生用
	AudioSource musicAudio;

	// SE再生番号
	int soundIndex = 0;
	
	void Awake() {
		Instance = this;

		// BGMの設定
		this.musicAudio = gameObject.AddComponent<AudioSource>();
		this.musicAudio.loop = true;
		this.musicAudio.volume = 0.5f;
		
		// SEのプーリング
		for (int i = 0; i < MaxSounds; i++) {
			this.soundAudioList.Add(gameObject.AddComponent<AudioSource>());
		}
		
		soundAudioClips[(int)Sound.Item]  = Resources.Load<AudioClip>("Sound/Triple Game Coin");
		musicAudioClips[(int)Music.Stage] = Resources.Load<AudioClip>("Music/bgm_maoudamashii_neorock35");
	}
	// SE再生
	public void PlaySound(Sound sound) {
		var audio = this.soundAudioList[soundIndex];
		audio.Stop();
		audio.clip = soundAudioClips[(int)sound];
		audio.Play();
		
		soundIndex++;
		soundIndex %= MaxSounds;
	}
	// BGM再生
	public void PlayMusic(Music music) {
		this.musicAudio.Stop();
		this.musicAudio.clip = musicAudioClips[(int)music];
		this.musicAudio.Play();
	}
}

public enum Sound {
	Item,
	Count	// SEの項目数
}
public enum Music {
	Stage,
	Count	// BGMの項目数
}
