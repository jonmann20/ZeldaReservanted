using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc;
	GameObject swordSwingSrcHolder, swordShootSrcHolder;

	void Start () {
		swordSwingSrcHolder = new GameObject();
		swordShootSrcHolder = new GameObject();

		swordSwingSrc = swordSwingSrcHolder.AddComponent<AudioSource>();
		swordSwingSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/swordSwing");

		swordShootSrc = swordShootSrcHolder.AddComponent<AudioSource>();
		swordShootSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/fullPowerSword");
	}

	public static void playSwordSwing(){
		swordSwingSrc.audio.Play();
	}

	public static void playSwordShoot(){
		swordShootSrc.audio.Play();
	}
}
