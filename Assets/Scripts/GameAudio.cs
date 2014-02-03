using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc, rupeePickupSrc, rupeePickup5Src, textSrc,
								enemyZapSrc;
	GameObject swordSwingSrcHolder, swordShootSrcHolder, rupeePickupSrcHolder, rupeePickup5SrcHolder, textSrcHolder,
								enemyZapSrcHolder;
					

	void Start () {
		swordSwingSrcHolder = new GameObject();
		swordShootSrcHolder = new GameObject();
		rupeePickupSrcHolder = new GameObject();
		rupeePickup5SrcHolder = new GameObject();
		textSrcHolder = new GameObject();
		enemyZapSrcHolder = new GameObject();

		swordSwingSrc = swordSwingSrcHolder.AddComponent<AudioSource>();
		swordShootSrc = swordShootSrcHolder.AddComponent<AudioSource>();
		rupeePickupSrc = rupeePickupSrcHolder.AddComponent<AudioSource>();
		rupeePickup5Src = rupeePickup5SrcHolder.AddComponent<AudioSource>();
		textSrc = textSrcHolder.AddComponent<AudioSource>();
		enemyZapSrc = enemyZapSrcHolder.AddComponent<AudioSource>();

		swordSwingSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/swordSwing");
		swordShootSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/fullPowerSword");
		rupeePickupSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/menuBeep");
		rupeePickup5Src.clip = Resources.Load<AudioClip>("Audio/soundEffects/rupees");
		textSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/text");
		enemyZapSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
	}

	public static void playSwordSwing(){
		swordSwingSrc.audio.Play();
	}

	public static void playSwordShoot(){
		swordShootSrc.audio.Play();
	}

	public static void playText()
	{
		textSrc.audio.Play();
	}

	public static void playEnemyZap()
	{
		enemyZapSrc.audio.Play();
	}

	public static void playRupeePickup(int num){

		if(num == 1){
			rupeePickupSrc.audio.Play();
		}
		else {
			// TODO: make sound math number of rupees
			rupeePickup5Src.audio.Play();
		}

	}
}
