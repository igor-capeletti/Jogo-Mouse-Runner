using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gato : MonoBehaviour {
	
	//objeto dentro do objeto Audio(audio do som do gato).
	public GameObject SomGato;

	//objeto cachorro
	public GameObject Cachorro;
	public GameObject SomCachorro;
	public GameObject LatidoOff;
	public GameObject LatidoOn;

	//objeto do Player.
	public GameObject Rato;

	//variavel deste script mas que é global.
	public float velocidade;
	public bool isColide;


//-------------------Script do Gato Malvado-------------------------

	void Start(){
		isColide = false;
	}

	//esta função atualiza constantemente tudo o que está dentro.
	void Update () {

		//esta variável receve o valor da variável velocidade do Personagem.
		velocidade = Rato.GetComponent<Player>().velocidade-(0.1f);
		if(isColide == true){
			GetComponent<SpriteRenderer>().flipX = true;
			velocidade= velocidade*(-1);
		}
		
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		
		//faz gato se mover na direção do rato.
		rigidbody.velocity= new Vector2((velocidade),rigidbody.velocity.y);

		//condição para desabilitar audio do gato quando jogo der Game Over.
		if(Rato.GetComponent<Player>().gameOver == true){
			SomGato.SetActive(false);
		}
	}

	//função para tivar sons do gato e do cachorro quando os dois se visualizarem.
	public void OnTriggerEnter2D(Collider2D collision2D)
	{	
		//condição que ativa som do cachorro e do gato ao se verem e quando colidirem com objeto específico invisível..
		if(collision2D.gameObject.CompareTag("latido")){
			SomCachorro.SetActive(false);
			SomCachorro.SetActive(true);
			SomGato.SetActive(false);
			SomGato.SetActive(true);
		}

		//condição de ativar e desativar colisores para trabalhar com os sons do gato e cachorro.
		if(collision2D.gameObject.CompareTag("cachorro")){
			isColide = true;
			LatidoOff.SetActive(true);
			LatidoOn.SetActive(false);
		}

		//condição para desativar sons do gato e do cachorro quando colidirem com objeto específico invisível.
		if(collision2D.gameObject.CompareTag("latidooff")){
			SomCachorro.SetActive(false);
			SomGato.SetActive(false);
		}
	}
}