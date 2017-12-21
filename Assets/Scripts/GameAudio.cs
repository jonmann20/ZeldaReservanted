using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static AudioSource swordSwingSrc, swordShootSrc, rupeePickupSrc, rupeePickup5Src, textSrc,
								enemyZapSrc, heartPickupSrc, itemObtainedSrc, itemReceivedSrc, doorOpenedSrc,
								gameOverSrc, playerHurtSrc, stairsSrc, secretSrc, triforceSrc;
	GameObject swordSwingSrcHolder, swordShootSrcHolder, rupeePickupSrcHolder, rupeePickup5SrcHolder, textSrcHolder,
								enemyZapSrcHolder, heartPickupSrcHolder, itemObtainedSrcHolder, itemReceivedSrcHolder,
	gameOverSrcHolder, playerHurtSrcHolder, stairsSrcHolder, secretSrcHolder, doorOpenedSrcHolder, triforceSrcHolder;
					
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
		setSoundEffect(ref stairsSrcHolder, ref stairsSrc, "stairs");
		setSoundEffect(ref doorOpenedSrcHolder, ref doorOpenedSrc, "doorOpened");
		setSoundEffect(ref secretSrcHolder, ref secretSrc, "magical");
		setSoundEffect(ref triforceSrcHolder, ref triforceSrc, "triforcePickup");

	}
	
	void setSoundEffect(ref GameObject holder, ref AudioSource src, string clip){
		holder = new GameObject();
		src = holder.AddComponent<AudioSource>();
		src.playOnAwake = false;
		src.clip = Resources.Load<AudioClip>("Audio/soundEffects/" + clip);
	}

	public static void playItemObtained(){
		itemObtainedSrc.GetComponent<AudioSource>().Play();
	}

	public static void playTriforce(){
		triforceSrc.GetComponent<AudioSource>().Play();
	}

	public static void playDoorOpened(){
		doorOpenedSrc.GetComponent<AudioSource>().Play();
	}

	public static void playItemReceived(){
		itemReceivedSrc.GetComponent<AudioSource>().Play();
	}

	public static void playSwordSwing(){
		swordSwingSrc.GetComponent<AudioSource>().Play();
	}

	public static void playSwordShoot(){
		swordShootSrc.GetComponent<AudioSource>().Play();
	}

	public static void playText()
	{
		textSrc.GetComponent<AudioSource>().Play();
	}

	public static void playEnemyZap()
	{
		enemyZapSrc.GetComponent<AudioSource>().Play();
	}

	public static void playHeartPickup()
	{
		heartPickupSrc.GetComponent<AudioSource>().Play();
	}

	public static void playGameOver()
	{
		gameOverSrc.GetComponent<AudioSource>().Play();
	}

	public static void playPlayerHurt()
	{
		playerHurtSrc.GetComponent<AudioSource>().Play();
	}

	public static void playMagical()
	{
		secretSrc.GetComponent<AudioSource>().Play();
	}

	public static void playStairs()
	{
		stairsSrc.GetComponent<AudioSource>().Play();
	}

	public static void playRupeePickup(int num){

		if(num == 1){
			rupeePickupSrc.GetComponent<AudioSource>().Play();
		}
		else {
			// TODO: make sound math number of rupees
			rupeePickup5Src.GetComponent<AudioSource>().Play();
		}

	}
}
