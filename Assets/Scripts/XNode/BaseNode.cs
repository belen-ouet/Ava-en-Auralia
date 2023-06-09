using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node {

	public virtual string GetString()
	{
		return null;
	}

	public virtual Sprite GetSpriteL()
	{
		return null;
	}

	public virtual Sprite GetSpriteR()
	{
		return null;
	}

	public virtual Sprite GetSpriteBubble()
	{
		return null;
	}

	public virtual Sprite GetSpriteNotebook()
	{
		return null;
	}
}