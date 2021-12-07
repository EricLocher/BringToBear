using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public static void DoCollision(Rigidbody2D p1, Rigidbody2D p2)
	{
		float p1Speed = p1.velocity.magnitude;
		float p2Speed = p2.velocity.magnitude;

		Vector3 p1Angle = p1.velocity.normalized;
		Vector3 p2Angle = p2.velocity.normalized;

		float p1damagePercentage = p1.GetComponent<PlayerController>().playerDamage / 100;
		float p2damagePercentage = p2.GetComponent<PlayerController>().playerDamage / 100;

		if (p1Speed > p2Speed)
		{
			p2Speed *= 0.5f;
		}
		else
		{
			p1Speed *= 0.5f;
		}

		p1.AddForce(p2Speed * p2Angle * p1damagePercentage, ForceMode2D.Impulse);
		p2.AddForce(p1Speed * p1Angle * p2damagePercentage, ForceMode2D.Impulse);

		p1.GetComponent<PlayerController>().invincible = true;
	}

	//GetKnockBackPercentage()
	//{
	//	return damage / maxDamage;
	//}
}
