using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject displays;

    public List<Sprite> sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingSprites());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                bool f = true;
                while (f)
                {
                    Sprite randomSprite = sprites[Random.Range(0, sprites.Count)];
                    if (randomSprite.name != hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite.name)
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = randomSprite;
                        f = false;
                    }
                }
            }
        }
    }

	public IEnumerator LoadingSprites()
	{
		// LoadSprites: 0=HandR 1=HandL 2=LegR 3=LegL 4=Head 5=Hair 6=Face 7=Body Loop by following the rule
		yield return StartCoroutine(ModifiedIMGLoader.LoadAndCropAllFilesToSprites(Application.streamingAssetsPath + "/Image/Dogs/"));
        Debug.Log("finished loading");
        Debug.Log(ModifiedIMGLoader.returnResults_Sprites.Count);
        sprites.Clear();
		foreach (Sprite s in ModifiedIMGLoader.returnResults_Sprites)
        {
            sprites.Add(s);
        }

		yield return "Loaded sprites";
        //if (onLoadFinished != null) onLoadFinished.Invoke();

        if (sprites.Count > 0) {
            for (int i = 0; i < displays.transform.childCount; i++)
            {
                displays.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
            }
        }
	}
}
