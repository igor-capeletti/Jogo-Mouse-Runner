using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//componentes de audio dentro do objeto Audio.
	public GameObject Audios;
	public GameObject SomInicio;
	public GameObject SomMusica;
	public GameObject SomPause;
	public GameObject SomGameOver;

	//objetos do Player.
	public GameObject jogador;

	//variáveis deste script mas que são globais.
	public float vidasJogador;
	public bool isSomOff;
	public bool isInicio= true;
	public bool isPaused = false;
	public bool isContinuar;
	public bool isReset = false;
	public bool isGameOver;

	//objetos do Canvas.
	public GameObject Img_vida3;
	public GameObject Img_vida2;
	public GameObject Img_vida1;
	public GameObject Canvas;
	public GameObject SomOff;
	public GameObject Painel_Inicio;
	public GameObject Painel_Pause;
	public GameObject GameOver;
	public GameObject Painel_Final;


//------------------Aqui começa o show---------------------------------
	//função de inicio.
	void Start () {
		Painel_Inicio.SetActive(true);
		SomInicio.SetActive(true);
		isPaused= true;
		isContinuar= false;	
	}
	
	//esta função atualiza constantemente tudo o que está dentro.
	void Update () {
		//recebe duas variáveis do script Player
		vidasJogador = jogador.GetComponent<Player>().vidas;
		isGameOver = jogador.GetComponent<Player>().gameOver;

		//exibe na tela 3 caretas de vida.
		if(vidasJogador == 3){
			Img_vida1.SetActive(false);
			Img_vida2.SetActive(false);
			Img_vida3.SetActive(true);
		
		//exibe na tela 2 caretas de vida.
		}else if(vidasJogador == 2){
			Img_vida1.SetActive(false);
			Img_vida2.SetActive(true);
			Img_vida3.SetActive(false);
		
		//exibe na tela 1 careta de vida.
		}else if(vidasJogador == 1){
			Img_vida1.SetActive(true);
			Img_vida2.SetActive(false);
			Img_vida3.SetActive(false);
		
		//não exibe nenhuma careta de vida.
		}else if(vidasJogador <= 0){
			Img_vida1.SetActive(false);
			Img_vida2.SetActive(false);
			Img_vida3.SetActive(false);
		}

		//condição para setar variável que indica que o personagem está andando ou parado.
		if(isPaused == false){
			isContinuar = true;
		}else if(isPaused == true){
			isContinuar = false;
		}

		/*condição para tocar a musica do jogo quando a variavél isContinuar estiver ativa
		OBS: esta variável isContinuar só é ativa quando não tem nenhum 
		painel de Game Over, Pause ou de Inicio sendo exibidos na tela.*/
		if(isContinuar == true){
			SomMusica.SetActive(true);
		}else if(isContinuar == false){
			SomMusica.SetActive(false);
		}

		//ativa painel de Game Over do jogo.
    	if(isGameOver == true){
    		SomMusica.SetActive(false);
    		SomPause.SetActive(false);
    		SomInicio.SetActive(false);
    		SomGameOver.SetActive(true);
    		isContinuar = false;
    		GameOver.SetActive(true);
    	}else{
    		GameOver.SetActive(false);
    	}

    	//condição para ativar a tela de final do nível, quando player chegar ao final.
    	if(jogador.GetComponent<Player>().isFinal == true){
    		SomMusica.SetActive(false);
    		SomInicio.SetActive(true);
    		Painel_Final.SetActive(true);
    	}
	}

	//função para exibir painel de inicio do jogo.
	public void Inicio(){
		if(isInicio){
    		Painel_Inicio.SetActive(false);
    		isInicio = false;
    		isPaused = false;
    		SomInicio.SetActive(false);
    	}else{
    		Painel_Inicio.SetActive(true);
    		isInicio = true;
    		isPaused = true;
    	}
	}

	//função de pausar o jogo e exibir tela de pause.
    public void Pause(){
    	if(isPaused){
    		Painel_Pause.SetActive(false);
    		isPaused = false;
    	}else{
    		Painel_Pause.SetActive(true);
    		isPaused = true;
    		SomPause.SetActive(true);
    	}

    }

    //função de continuar o jogo após estar pausado e desativar tela de pause.
    public void PauseOF(){
    	if(isContinuar== false){
    		Painel_Pause.SetActive(false);
    		isPaused = false;
    		SomPause.SetActive(false);
    	}else{
    		Painel_Pause.SetActive(true);
    		isPaused = true;
    	}
    }

    //função de desativar o som.
    public void SOMOOFF(){
    	if(isSomOff){
    		SomOff.SetActive(false);
    		isSomOff= false;
    	}else{
    		SomOff.SetActive(true);
    		isSomOff= true;
    		Audios.SetActive(false);
    	}
    }

    //função de ativar o som.
    public void SOMOON(){
    	if(isSomOff){
    		SomOff.SetActive(false);
    		isSomOff= false;
    		Audios.SetActive(true);
    	}else{
    		SomOff.SetActive(true);
    		isSomOff= true;
    	}
    }

    //função de reiniciar o jogo.
    public void RESET(){
    	if(isReset){
    		Painel_Pause.SetActive(false);
    		isReset = false;
    	}else{
    		Painel_Pause.SetActive(true);
    		isReset = true;
    		Application.LoadLevel(Application.loadedLevel);
    	}
    }

    //função de sair do jogo.
    public void SAIR(){
    	Application.Quit(); //só funciona depois do jogo compilado.
    }
}