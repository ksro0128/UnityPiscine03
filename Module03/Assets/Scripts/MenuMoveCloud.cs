using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMoveCloud : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private float rightBound = 30f;
	[SerializeField] private float leftBound = -45f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
		if (transform.position.x > rightBound)
			transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
    }
}
