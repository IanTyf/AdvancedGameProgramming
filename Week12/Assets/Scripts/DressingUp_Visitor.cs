using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressingUp_Visitor : MonoBehaviour {

    [SerializeField]
    protected SpriteRenderer spriteRender_Body;
    [SerializeField]
	protected SpriteRenderer spriteRender_Hair;
    [SerializeField]
	protected SpriteRenderer spriteRender_Head;
    [SerializeField]
	protected SpriteRenderer spriteRender_Face;
    [SerializeField]
	protected SpriteRenderer spriteRender_LegsL;
    [SerializeField]
	protected SpriteRenderer spriteRender_LegsR;
    [SerializeField]
	protected SpriteRenderer spriteRender_HandL;
    [SerializeField]
	protected SpriteRenderer spriteRender_HandR;
	[SerializeField]
	protected SpriteRenderer spriteRender_Camera;
	//[SerializeField]
 //   Vector2 hueRange_Body = new Vector2(0, 359.99f);
 //   [SerializeField]
 //   Vector2 hueRange_Hair = new Vector2(-33.33f, 33.33f);
	[SerializeField]
	protected float outlineSize = 20;

	[SerializeField]
	SpriteRenderer[] spritesRender_OutlinePlus;

	[SerializeField]
	bool changedBody = true;


	//For cosplayer and special characters, change character sprites to the target combo
	public void ClothesSetUp(Sprite sprite_Body, Sprite sprite_Hair, Sprite sprite_Head, Sprite sprite_Face, Sprite sprite_Leg_L, Sprite sprite_Leg_R, Sprite sprite_Hand_L, Sprite sprite_Hand_R)
	{
		spriteRender_Body.sprite = sprite_Body;
		spriteRender_Hair.sprite = sprite_Hair;
		spriteRender_Head.sprite = sprite_Head;
		spriteRender_Face.sprite = sprite_Face;
		spriteRender_LegsL.sprite = sprite_Leg_L;
		spriteRender_LegsR.sprite = sprite_Leg_R;
		spriteRender_HandL.sprite = sprite_Hand_L;
		spriteRender_HandR.sprite = sprite_Hand_R;
	}
}
