using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Classe responsável por capturar os inputs do jogador
public class PlayerInputs : MonoBehaviour
{
    // Referência ao joystick virtual utilizado para movimentação
    [SerializeField] private FixedJoystick digitalAnalogic;

    // Vetor que armazena a direção do input do jogador
    private Vector2 inputDirection;

    // Referência ao botão de bomba na interface
    [SerializeField] private Button bombButton;

    // Variável que indica se o botão da bomba foi pressionado
    private bool bombPressed = false;

    // Propriedade para acessar e modificar o estado do botão da bomba
    public bool BombPressed
    {
        get { return bombPressed; }
        set { bombPressed = value; }
    }

    // Método chamado a cada frame para atualizar a movimentação
    private void Update()
    {
        MovimentInput();
    }

    // Retorna a direção do input do jogador
    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }

    // Captura a direção do joystick virtual e armazena no inputDirection
    public void MovimentInput()
    {
        inputDirection = digitalAnalogic.Direction;
    }
}