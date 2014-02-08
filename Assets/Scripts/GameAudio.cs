using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc, rupeePickupSrc, rupeePickup5Src, textSrc,
								enemyZapSrc, heartPickupSrc, itemObtainedSrc, itemReceivedSrc,
								gameOverSrc, playerHurtSrc;
	GameObject swordSwingSrcHolder, swordShootSrcHolder, rupeePickupSrcHolder, rupeePickup5SrcHolder, textSrcHolder,
								enemyZapSrcHolder, heartPickupSrcHolder, itemObtainedSrcHolder, itemReceivedSrcHolder,
								gameOverSrcHolder, playerHurtSrcHolder;
					
	void Awake()
	{
		setSoundEffect(ref swordSwingSrcHolder, ref swordSwingSrc, "swordSwing");
		setSoundEffect(ref swordShootSrcHolder, ref swordShootSrc, "fullPowerSword");
		setSoundEffect(ref rupeePickupSrcHolder, ref rupeePickupSrc, "menuBeep");
		setSoundEffect(ref rupeePickup5SrcHolder, ref rupeePickup5Src, "rupees");
		setSoundEffect(ref textSrcHolder, ref textSrc, "text");
		setSoundEffect(ref enemyZapSrcHolder, ref enemyZapSrc, "enemyZapped");
		setSoundEffect(ref heartPickupSrcHolder, ref heartPickupSrc, "healthHeart");
		setSoundEffect(ref itemObtainedSrcHolder, ref itemObtainedSrc, "itemObtained");
		setSoundEffect(ref itemReceivedSrcHolder, ref itemReceivedSrc, "itemReceived");
		setSoundEffect(ref gameOverSrcHolder, ref gameOverSrc, "gameOver");
		setSoundEffect(ref playerHurtSrcHolder, ref playerHurtSrc, "playerHurt");
	}
	
	void setSoundEffect(ref GameObject holder, ref AudioSource src, string clip){
		holder = new GameObject();
		src = holder.AddComponent<AudioSource>();
		src.playOnAwake = false;
		src.clip = Resources.Load<AudioClip>("Audio/soundEffects/" + clip);
	}

	public static void playItemObtained(){
		itemObtainedSrc.audio.Play();
	}

	public static void playItemReceived(){
		itemReceivedSrc.audio.Play();
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

	public static void playHeartPickup()
	{
		heartPickupSrc.audio.Play();
	}

	public static void playGameOver()
	{
		gameOverSrc.audio.Play();
	}

	public static void playPlayerHurt()
	{
		playerHurtSrc.audio.Play();
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
