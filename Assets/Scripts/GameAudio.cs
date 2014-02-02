﻿using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc, rupeePickupSrc, rupeePickup5Src;
	GameObject swordSwingSrcHolder, swordShootSrcHolder, rupeePickupSrcHolder, rupeePickup5SrcHolder;

	void Start () {
		swordSwingSrcHolder = new GameObject();
		swordShootSrcHolder = new GameObject();
		rupeePickupSrcHolder = new GameObject();
		rupeePickup5SrcHolder = new GameObject();

		swordSwingSrc = swordSwingSrcHolder.AddComponent<AudioSource>();
		swordShootSrc = swordShootSrcHolder.AddComponent<AudioSource>();
		rupeePickupSrc = rupeePickupSrcHolder.AddComponent<AudioSource>();
		rupeePickup5Src = rupeePickup5SrcHolder.AddComponent<AudioSource>();

		swordSwingSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/swordSwing");
		swordShootSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/fullPowerSword");
		rupeePickupSrc.clip = Resources.Load<AudioClip>("Audio/soundEffects/menuBeep");
		rupeePickup5Src.clip = Resources.Load<AudioClip>("Audio/soundEffects/rupees");
	}

	public static void playSwordSwing(){
		swordSwingSrc.audio.Play();
	}

	public static void playSwordShoot(){
		swordShootSrc.audio.Play();
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
