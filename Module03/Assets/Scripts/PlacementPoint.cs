using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPoint : MonoBehaviour
{
    public bool isOccupied = false;
    private SpriteRenderer borderRenderer;

    void Start()
    {
        // 사각형 테두리를 만들고 비활성화
        GameObject border = new GameObject("Border");
        border.transform.SetParent(transform);
        border.transform.localPosition = Vector3.zero;
        border.transform.localScale = Vector3.one * 3f;

        borderRenderer = border.AddComponent<SpriteRenderer>();
        borderRenderer.sprite = CreateRectangleBorder();
        borderRenderer.sortingOrder = 3; // 유닛 위에 보이도록 설정
        HideBorder();

		var collider = gameObject.AddComponent<BoxCollider2D>();
		collider.size = new Vector2(1, 1);
		collider.isTrigger = true;
    }

    public void ShowBorder()
    {
        borderRenderer.color = !isOccupied ? Color.green : Color.red;
    }

    public void HideBorder()
    {
        borderRenderer.color = Color.clear;
    }

    private Sprite CreateRectangleBorder()
    {
        int width = 32;
        int height = 32;
        Texture2D texture = new Texture2D(width, height);
        
        // 모든 픽셀을 투명하게 설정
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }

        // 테두리 픽셀을 흰색으로 설정
        for (int x = 0; x < width; x++)
        {
            pixels[x] = Color.white; // 상단
            pixels[x + (height - 1) * width] = Color.white; // 하단
        }
        for (int y = 0; y < height; y++)
        {
            pixels[y * width] = Color.white; // 좌측
            pixels[(y + 1) * width - 1] = Color.white; // 우측
        }

        texture.SetPixels(pixels);
        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
	
	public void SetOccupied(bool occupied)
	{
		isOccupied = occupied;
	}

	public void CheckPoint()
	{
		if (!isOccupied)
		{
			borderRenderer.color = Color.blue;
		}		
	}
}
