using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretSelectController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private GameObject turretPrefab;
	[SerializeField] private int turretCost = 100;
	[SerializeField] private PlacementPoint[] placementPoints;
	[SerializeField] private GameObject rangeIndicator;
	[SerializeField] private TMPro.TextMeshProUGUI TurretInfoText;
	[SerializeField] private GameObject TurretInfoPanel;


	private PlacementPoint currentPlacementPoint;
	private bool isDragging = false;
	private bool isCoolDown = false;

	private GameObject turret;
	private GameObject ri;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (StageManager.instance.GetMoney() < turretCost)
		{
			Debug.Log("Not enough money");
			return;
		}

		if (isCoolDown)
			return;
		ShowAllBorders();
			
		turret = Instantiate(turretPrefab);
		ri = Instantiate(rangeIndicator);
		ri.transform.localScale = turret.GetComponent<Turret>().GetDetectionRadius() * 2;
		turret.GetComponent<Turret>().SetActive(false);
		isDragging = true;

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isDragging)
			return ;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		turret.transform.position = mousePosition;
		ri.transform.position = mousePosition;
		CheckPlacementPoint(mousePosition);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!isDragging)
			return ;
		if (currentPlacementPoint != null)
		{
			GameObject mapTurret = Instantiate(turretPrefab, currentPlacementPoint.transform.position, Quaternion.identity);
			mapTurret.GetComponent<Turret>().SetActive(true);
			StageManager.instance.RemoveMoney(turretCost);
			currentPlacementPoint.isOccupied = true;
			StartCoroutine(CoolDown());			
		}
		Destroy(turret);
		Destroy(ri);
		HideAllBorders();
		isDragging = false;
		currentPlacementPoint = null;
	}

	IEnumerator CoolDown()
	{
		isCoolDown = true;
		float coolTime = turretPrefab.GetComponent<Turret>().GetCoolTime();
		Button button = GetComponent<Button>();
		ColorBlock cb = button.colors;
		ColorBlock orig = button.colors;
		cb.normalColor = Color.gray;
		cb.highlightedColor = Color.gray;
		cb.pressedColor = Color.gray;
		cb.disabledColor = Color.gray;
		button.colors = cb;
		button.interactable = false;
		yield return new WaitForSeconds(coolTime);
		button.colors = orig;
		button.interactable = true;
		isCoolDown = false;
	}

	private void ShowAllBorders()
	{
		foreach (var point in placementPoints)
		{
			point.ShowBorder();
		}
	}

	private void HideAllBorders()
	{
		foreach (var point in placementPoints)
		{
			point.HideBorder();
		}
	}

	private void CheckPlacementPoint(Vector3 mousePosition)
	{
		currentPlacementPoint = null;
		CheckCurrentPlacementPoint();
		foreach (var point in placementPoints)
		{
			if (point.isOccupied)
				continue;
			if (Vector3.Distance(mousePosition, point.transform.position) < 1f)
			{
				currentPlacementPoint = point;
				CheckCurrentPlacementPoint();
			}
		}
	}

	private void CheckCurrentPlacementPoint()
	{
		foreach (var point in placementPoints)
		{
			point.ShowBorder();
		}
		if (currentPlacementPoint != null)
		{
			currentPlacementPoint.CheckPoint();
		}
	}

	public void OnPointerEnter()
	{
		TurretInfoPanel.SetActive(true);
		float damage = turretPrefab.GetComponent<Turret>().GetDamage() + turretPrefab.GetComponent<Turret>().GetBulletDamage();
		float fireRate = turretPrefab.GetComponent<Turret>().GetFireRate();
		float range = turretPrefab.GetComponent<Turret>().GetRange();
		float coolTime = turretPrefab.GetComponent<Turret>().GetCoolTime();
		TurretInfoText.text = "" + turretPrefab.name + "\nDamage: " + damage + "\nFire Rate: " + fireRate + "\nRange: " + range + "\nCool Time: " + coolTime;
	}

	public void OnPointerExit()
	{
		TurretInfoPanel.SetActive(false);
		TurretInfoText.text = "";
	}
}
