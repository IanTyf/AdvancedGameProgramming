using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterSpriteSet
{
	//For special stall holders (agents)
	public Sprite sBody;
	public Sprite sHead;
	public Sprite sHair;
	public Sprite sFace;
	public Sprite sLegsL;
	public Sprite sLegsR;
	public Sprite sHandL;
	public Sprite sHandR;


	public void ClothesSetUp(Sprite sprite_Body, Sprite sprite_Hair, Sprite sprite_Head, Sprite sprite_Face, Sprite sprite_Leg_L, Sprite sprite_Leg_R, Sprite sprite_Hand_L, Sprite sprite_Hand_R)
	{
		sBody = sprite_Body;
		sHair = sprite_Hair;
		sHead = sprite_Head;
		sFace = sprite_Face;
		sLegsL = sprite_Leg_L;
		sLegsR = sprite_Leg_R;
		sHandL = sprite_Hand_L;
		sHandR = sprite_Hand_R;
	}
}
