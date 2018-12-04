using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Image load;

	public bool loading = false;

	public void LoadScreen() {
		load.enabled = true;
		loading = true;
	}

	public void FinishLoad() {
		load.enabled = false;
		loading = false;
	}

}
