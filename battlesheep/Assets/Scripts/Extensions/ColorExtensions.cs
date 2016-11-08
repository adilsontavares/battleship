using UnityEngine;

public static class ColorExtensions
{
	public static Color WithAlpha(this Color color, float alpha)
	{
		var newColor = new Color(color.r, color.g, color.b, alpha);
		return newColor;
	}
}
