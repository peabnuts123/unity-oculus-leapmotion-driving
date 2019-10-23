using UnityEngine;
using System.Collections;

public class OpenElement : MonoBehaviour {

	// on définit une variable privée qui va contenir le composant Animator de l'objet sur lequel est appliqué le script
	private Animator Opening;
	public KeyCode keyboard;

	// Fonction Start se lance 1 fois au lancement du jeu
	void Start () {
		Opening = GetComponent<Animator>();
	}

	// Fonction Update s'execute à chaque frame (si tu tourne à 60FPS elles s'éxecute 60 fois en 1 seconde.
	void Update () {

		if (Input.GetKeyDown(keyboard)) {
			// on reccupère la variable EtatPorte du controlleur d'animation, et on lui affecte la valeur 1
			if (Opening.GetInteger ("EtatAnim") == 1) {
				Opening.SetInteger("EtatAnim",2);
			} else {
				Opening.SetInteger("EtatAnim",1);
			};
		}
	}
}
