using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc, rupeePickupSrc;
	GameObject swordSwingSrcHolder, swordShootSrcHolder, rupeePickupSrcHolder;

	void Start () {
		swordSwingSrcHolder = new GameObject();
		swordShootSrcHolder = new GameObject();
		rupeePickupSrcHolder = new GameObject();

		swordSwingSrc = swordSwingSrcHolder.AddComponent<AudioSource>();
		swordShootSrc = swordShootSrcHolder.AddComponent<AudioSource>();
		rupeePickupSrc = rupeePickupSrcHolder.AddComponent<AudioSource>();

		swordSwingSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/swordSwing");
		swordShootSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/fullPowerSword");
		rupeePickupSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/rupees");
	}

	public static void playSwordSwing(){
		swordSwingSrc.audio.Play();
	}

	public static void playSwordShoot(){
		swordShootSrc.audio.Play();
	}

	public static void playRupeePickup(int num){
		// TODO: make sound math number of rupees
		rupeePickupSrc.audio.Play();
	}


}
