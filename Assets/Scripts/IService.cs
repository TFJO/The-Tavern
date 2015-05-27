using UnityEngine;
using System.Collections;

public interface IService {


	bool IsBlocked();
	bool ProvideService(AIController controller);
	AIController GetCurrentServiceUser();
	Transform GetTransform();
	AIController.Need GetProvidedNeed();
	bool Claim();
	bool IsClaimed();
	void Recharge();
}
