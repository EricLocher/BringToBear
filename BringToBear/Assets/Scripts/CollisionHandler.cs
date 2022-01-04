using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] GameObject CollisionAnimation;
	static GameObject Collision;
	private void Start()
    {
		Collision = CollisionAnimation;
    }

	public static void DoCollision(Rigidbody2D p1, Rigidbody2D p2, float shieldForce, Vector2 impactPoint)
	{
		float p1Speed = 1 + p1.velocity.magnitude;
		float p2Speed = 1 + p2.velocity.magnitude;

		Vector3 p1Angle = new Vector3(1, 1) + (Vector3)p1.velocity.normalized;
		Vector3 p2Angle = new Vector3(1, 1) + (Vector3)p2.velocity.normalized;

		float p1damagePercentage = 1 + p1.GetComponent<PlayerController>().damageTaken / 1500;
		float p2damagePercentage = 1 + p2.GetComponent<PlayerController>().damageTaken / 1500;

		if (p1Speed > p2Speed)
		{
			p2Speed *= 0.5f;
		}
		else
		{
			p1Speed *= 0.5f;
		}
		Debug.Log(p2Speed + ":"+ p2Angle +":"+ p1damagePercentage +":"+ shieldForce);
		Debug.Log(p1Speed + ":" + p1Angle + ":" + p2damagePercentage + ":" + shieldForce);
		p1.AddForce(p2Speed * (Vector2)p2Angle * p1damagePercentage * shieldForce, ForceMode2D.Impulse);
		p2.AddForce(p1Speed * (Vector2)p1Angle * p2damagePercentage * shieldForce, ForceMode2D.Impulse);

		p1.GetComponent<PlayerController>().invincible = true;

		Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		Instantiate(Collision, impactPoint, _rotation);

	}

	//GetKnockBackPercentage()
	//{
	//	return damage / maxDamage;
	//}
}
