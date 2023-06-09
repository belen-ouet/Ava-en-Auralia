using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;
	public string speakerName;
	public string dialogueLine;
	public Sprite spriteR;
	public Sprite spriteL;
	public Sprite spriteBubble;
	public Sprite spriteNotes;
	public string textNotes;

	public override string GetString()
	{
		return "DialogueNode/" + speakerName + "/" + dialogueLine + "/" + textNotes;
	}

	public override Sprite GetSpriteL()
	{
		return spriteL;
	}

	public override Sprite GetSpriteR()
	{
		return spriteR;
	}

	public override Sprite GetSpriteBubble()
	{
		return spriteBubble;
	}

	public override Sprite GetSpriteNotebook()
	{
		return spriteNotes;
	}
}