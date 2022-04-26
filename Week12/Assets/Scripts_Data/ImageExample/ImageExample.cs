using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageExample : MonoBehaviour
{
	public delegate void OnLoadFinished();
	public OnLoadFinished onLoadFinished;
	[SerializeField]
	CharacterSetDB characterSetDB;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(LoadingClothes());
    }

	public IEnumerator LoadingClothes()
	{
		// LoadSprites: 0=HandR 1=HandL 2=LegR 3=LegL 4=Head 5=Hair 6=Face 7=Body Loop by following the rule
		yield return StartCoroutine(IMG2Sprites.LoadAndCropAllFilesToSprites(Application.streamingAssetsPath + "/Image/Cosplayers/"));
		List<Sprite> allCustomCosplayers = IMG2Sprites.returnResults_Sprites;

		Sprite[] allEmbedCosplayers = Resources.LoadAll<Sprite>("Cosplayers/Random/");

		Sprite[] allCosplayerSprite;
		int currentIndex = 0;

		//If there are player customized characters
		if (allCustomCosplayers.Count > 0)
		{
			//Create sprites array size
			allCosplayerSprite = new Sprite[allCustomCosplayers.Count + allEmbedCosplayers.Length];

			for (int i = 0; i < allCustomCosplayers.Count; i++)
			{
				allCosplayerSprite[i] = allCustomCosplayers[i];
			}
			currentIndex = allCustomCosplayers.Count;
		}
		else
		{
			//Create sprites array size
			allCosplayerSprite = new Sprite[allEmbedCosplayers.Length];
		}

		//Add embed characters
		for (int i = 0; i < allEmbedCosplayers.Length; i++)
		{
			//Accumulated by currentIndex, multiple sources' clothes
			allCosplayerSprite[currentIndex + i] = allEmbedCosplayers[i];
		}

		characterSetDB.cosplayerSet.Clear();
		//Sort sprites
		for (int i = 0; i < allCosplayerSprite.Length; i += 8)
		{
			CharacterSpriteSet characterSpriteSet = new CharacterSpriteSet();
			characterSpriteSet.ClothesSetUp(allCosplayerSprite[i + 7], allCosplayerSprite[i + 5], allCosplayerSprite[i + 4],
				allCosplayerSprite[i + 6], allCosplayerSprite[i + 3], allCosplayerSprite[i + 2], allCosplayerSprite[i + 1], allCosplayerSprite[i + 0]);
			characterSetDB.cosplayerSet.Add(characterSpriteSet);
		}
		yield return "Loaded clothes";
		if (onLoadFinished != null) onLoadFinished.Invoke();
	}
}
