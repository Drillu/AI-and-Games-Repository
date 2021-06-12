using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
	public enum FitType
	{
		Uniform,
		Width,
		Height,
		FixedRows,
		FixedColumns
	}

	public FitType fitType;

	public int rows;
	public int columns;

	public Vector2 spacing;

	public Vector2 cellSize;
	public bool fixedX;
	public bool fixedY;

	public override void SetLayoutHorizontal()
	{
		base.CalculateLayoutInputHorizontal();
		int childCount = transform.childCount;
		float sqrt = Mathf.Sqrt(childCount);

		int squareWidth = Mathf.CeilToInt(sqrt);
		switch(fitType)
		{
			case FitType.Uniform:
				rows = squareWidth;
				columns = squareWidth;
				break;
			case FitType.Width:
				columns = squareWidth;
				rows = Mathf.CeilToInt((float) childCount / columns);
				break;
			case FitType.Height:
				rows = squareWidth;
				columns = Mathf.CeilToInt((float) childCount / rows);
				break;
			case FitType.FixedRows:
				columns = Mathf.CeilToInt((float) childCount / rows);
				break;
			case FitType.FixedColumns:
				rows = Mathf.CeilToInt((float) childCount / columns);
				break;
			default:
				break;
		}

		float containerWidth = rectTransform.rect.width;
		float containerHeight = rectTransform.rect.height;

		//float cellWidth = (containerWidth - spacing.x * (columns - 1) - padding.left - padding.right / (float) columns) / columns;
		//float cellHeight = (containerHeight - spacing.y * (rows - 1) - padding.top - padding.bottom / (float) rows) / rows;

		float cellWidth = (containerWidth - spacing.x * (columns - 1) - padding.left - padding.right) / columns;
		float cellHeight = (containerHeight - spacing.y * (rows - 1) - padding.top - padding.bottom) / rows;

		cellSize.x = fixedX ? cellSize.x : cellWidth;
		cellSize.y = fixedY ? cellSize.y : cellHeight;

		int rowInd;
		int colInd;

		for(int i = 0; i < rectChildren.Count; i++)
		{
			RectTransform item = rectChildren[i];
			rowInd = i / columns;
			colInd = i % columns;

			// topleft position of the child recttransform
			float xPos = cellSize.x * colInd + colInd * spacing.x + padding.left;
			float yPos = cellSize.y * rowInd + rowInd * spacing.y + padding.top;

			SetChildAlongAxis(item, 0, xPos, cellSize.x);
			SetChildAlongAxis(item, 1, yPos, cellSize.y);
		}

	}

	public override void CalculateLayoutInputVertical()
	{
	}


	public override void SetLayoutVertical()
	{
	}
}
