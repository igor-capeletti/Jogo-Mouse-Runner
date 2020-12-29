using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	//compontente Canvas.
	public GameObject Painel;

	//componentes do objeto Audio.
	public GameObject SomPulo;
	public GameObject SomDano;
	public GameObject SomVida;
	public GameObject SomRatoeira;
	public GameObject SomVeneno;

	//variáveis deste script mas que são globais.
	public float forcaPulo;
	public float velocidade;
	public float velocidadeParado;
	public float velocidadeAndando;
	public float vidas;
	public bool estaChao;
	public bool duploPulo;
	public bool gameOver;
	public int destroyVeneno= 0;
	public bool isFinal=false;

	//componentes de veneno.
	public GameObject Veneno1;
	public GameObject Veneno2;
	public GameObject ativaVeneno;
	public GameObject desativaVeneno;

//------------------Aqui começa a luta pela vida do rato---------------------------------

	//função de inicio.
	void Start () {
		gameOver = false;
		velocidadeParado= 0;
		velocidadeAndando= 7;
	}
	
	//esta função atualiza constantemente tudo o que está dentro.
	void Update () {

		velocidade= velocidadeAndando;

		//condição para deixar personagem parado no jogo quando pausado.
		if(Painel.GetComponent<GameManager>().isPaused == true){
			velocidade = velocidadeParado;
			GetComponent<Rigidbody2D>().simulated = false;
		}else{
			velocidade = velocidadeAndando;
			GetComponent<Rigidbody2D>().simulated = true;
		}

		//personagem andar.
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity= new Vector2((velocidade),rigidbody.velocity.y);
		
		//condição para ativar animação do personagem quando estiver andando.
		if((velocidade > 0) && estaChao){
			GetComponent<Animator>().SetBool("andando",true);
		}
		
		//condição para receber a tecla Espaço e fazer o rato pular.
		if(Input.GetKey(KeyCode.Space) == true && estaChao == true){
			SomPulo.SetActive(false);
			SomPulo.SetActive(true);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(10,forcaPulo));
			estaChao= false;
			//duploPulo = true;
		}

		/*condição para liberar o pulo duplo para o usuário.
		if(Input.GetKeyUp(KeyCode.Space) && estaChao){
			estaChao= false;
		}

		condição para receber novamente a tecla Espaço e fazer o personagem dar pulo duplo.
		if(Input.GetKeyDown(KeyCode.Space) == true && estaChao == false && duploPulo == true){
			Debug.Log("Pulando 2!");
			SomPulo.SetActive(false);
			SomPulo.SetActive(true);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,forcaPulo));
			duploPulo= false;
		}
		*/

		/*condição para chamar tela Game Over quando vida for igual a zero.
		o script GameManager recebe esta variável, então quando ativada, o outro
		script ativa a tela de GameOver.*/
		if(vidas <= 0){
			velocidadeAndando = 0;
			gameOver= true;
		}

		//condição para desativar sons do rato quando o jogo der Game Over ou Pause.
		if(velocidade == 0 || vidas == 0 || isFinal == true){
			SomPulo.SetActive(false);
			SomDano.SetActive(false);
			SomVida.SetActive(false);
			SomRatoeira.SetActive(false);
			SomVeneno.SetActive(false);
		}
	}

	//função de colisões do rato com os objetos que ganham e perdem vida.
	private void OnTriggerEnter2D(Collider2D collision2D)
	{	
		//função de colisão com queijo amarelo para ganhar vida.
		if(collision2D.gameObject.CompareTag("queijo_vida")){
			SomVida.SetActive(false);
			SomVida.SetActive(true);
			Destroy(collision2D.gameObject);
			vidas++;
			if(vidas > 2){
				vidas=3;
			}	
		}

		//função de colisão com o queijo verde para perder 1 de vida.
		if(collision2D.gameObject.CompareTag("queijo_estragado")){
			SomDano.SetActive(false);
			SomDano.SetActive(true);
			Destroy(collision2D.gameObject);
			vidas--;
			if(vidas > 2){
				vidas=3;
			}
		}

		//função de colisão com o veneno para perder 1 de vida.
		if(collision2D.gameObject.CompareTag("veneno")){
			SomDano.SetActive(false);
			SomDano.SetActive(true);
			Veneno1.SetActive(false);
			Veneno2.SetActive(false);
			vidas--;
			if(vidas > 2){
				vidas=3;
			}
		}

		//função de colisão com a ratoeira para perder 1 de vida.
		if(collision2D.gameObject.CompareTag("ratoeira")){
			SomRatoeira.SetActive(false);
			SomRatoeira.SetActive(true);
			SomDano.SetActive(false);
			SomDano.SetActive(true);
			Destroy(collision2D.gameObject);
			vidas--;
			if(vidas > 2){
				vidas=3;
			}
		}

		//função de colisão com o rato morrer e aparecer Game Over no jogo.
		if(collision2D.gameObject.CompareTag("gato")){
			vidas= 0;
			gameOver= true;
		}

		/*condição de colisão com objetos invisiveis que aumentam a velocidade do rato.
		durante a fase.*/
		if(collision2D.gameObject.CompareTag("velocidades")){
			Destroy(collision2D.gameObject);
			velocidadeAndando+=1;
		}

		//condição de colisão com objetos invisiveis que ativam o sprite dos objetos venenos.
		if(collision2D.gameObject.CompareTag("venenoOn")){
			Veneno1.SetActive(true);
			Veneno2.SetActive(true);
			SomVeneno.SetActive(true);
			if(destroyVeneno > 1){
				Destroy(collision2D.gameObject);
			}
		}

		//condição de colisão com objetos invisiveis que desativam os sprite dos objetos venenos.
		if(collision2D.gameObject.CompareTag("venenoof")){
			Veneno1.SetActive(false);
			Veneno2.SetActive(false);
			SomVeneno.SetActive(false);
			destroyVeneno++;
			if(destroyVeneno > 1){
				Destroy(collision2D.gameObject);
			}
		}

		//condição de colisão com objeto invisivel quando player chegar no final do nível.
		if(collision2D.gameObject.CompareTag("final")){
			isFinal = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision2D){

	}

	/*funções de colisão com o chão e objetos que o personagem 
	pode ficar sobre eles ou travar neles durante o jogo.
	Se estão colidindo apenas setam a variavel "estaChao= true" para o personagem
	conseguir pular quando estiver por cima delas ou colidindo nelas.*/
	void OnCollisionEnter2D(Collision2D collision2D)
	{	
		//confirma colisão com o chão.
		if(collision2D.gameObject.CompareTag("chao")){
			estaChao = true;
		}

		//confirma colisão com os troncos.
		if(collision2D.gameObject.CompareTag("tronco")){
			estaChao = true;
		}

		//confirma colisão com as lixeiras.
		if(collision2D.gameObject.CompareTag("lixeira")){
			estaChao = true;
		}

		//confirma colisão com os cactos.
		if(collision2D.gameObject.CompareTag("cacto")){
			estaChao = true;
		}

		//confirma colisão com as latas de veneno.
		if(collision2D.gameObject.CompareTag("lata_veneno")){
			estaChao = true;
			duploPulo = false;
		}
	}
	
	/*funções de colisão que faz o contrário da função a cima.
	Se o rato não estiver mais colidindo com algum desses componentes, 
	apenas setam a variavel "estaChao= false" para o personagem não ficar pulando
	sem limites de pulos.*/
	void OnCollisionExit2D(Collision2D collision2D)
	{
	}

}