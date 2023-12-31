using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JosueMartin
{

    //Este personaje, va a caminar, correr, saltar y agacharse
    public class MovementController : MonoBehaviour
    {

        [Header("Variables de Movimiento")]
        [SerializeField] private float walkingSpeed;
        [SerializeField] private float runningSpeed;

        [SerializeField] private float jumpingForce;

        [SerializeField] private float crouchHeight;
        [SerializeField] private float crouchingSpeed;

        [Header("Deteccion del suelo")]
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private float rayRange;
        [SerializeField] private LayerMask rayLayers;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            Jump();
        }

        #region Movement

        // En los metodos, la primera letra de cada palabra en Mayuscula
        private void Move()
        {
            rb.velocity += new Vector3(HorizontalMove(), 0, VerticalMove()) * ActualSpeed() * Time.deltaTime;
        }

        // Variables cuyo valor puedes modificar dentro de las mismas

        private float HorizontalMove()
        {
            return Input.GetAxis("Horizontal"); // Si estoy presionando A va a regresar -1. Si presiono D va a regresar 1
        }

        private float VerticalMove()
        {
            return Input.GetAxis("Vertical"); // Si estoy presionando S va a regresar -1. Si presiono W va a regresar 1
        }

        private float ActualSpeed()
        {
            // Si estoy presionando el input de correr, va a regresar runningSpeed, si no va a regresar walkingSpeed
            return RunInputPressed() ? runningSpeed : walkingSpeed;
        }

        private bool RunInputPressed()
        {
            return Input.GetKey(KeyCode.LeftShift); // Si estoy presionando shift izquierdo va a regresar True, y si no Falso
        }

        #endregion

        #region Jump

        private void Jump()
        {
            if (JumpInput() && IsTouchingGround())
            {
                rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
            }
        }

        private bool JumpInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        /// <summary>
        /// Hay un raycast o rayo, que nos sirve para saber si el jugador esta tocando o no el suelo
        /// 
        /// El raycast, prinicpalmente necesita de
        /// 
        /// Origen: Es de donde sale el rayo
        /// 
        /// Direccion: Es hacia donde apunta el rayo
        /// 
        /// Rango: Es el alcance o longitud del rayo
        /// 
        /// Layers: Que layers puede o debe detectar el rayo
        /// 
        /// -------------------------------------------------------
        /// 
        /// Hit: Te devuelve el objeto exacto que esta tocando el rayo
        /// 
        /// </summary>
        private bool IsTouchingGround()
        {
            return Physics.Raycast(rayOrigin.position, Vector3.down, rayRange, rayLayers);
        }

        /// <summary>
        /// On Draw Gizmos unicamente dibuja lineas o figuras en el editor
        /// Estas lineas o figuras son meramente representativas, y no tienen ningun efecto sobre el juego
        /// 
        /// NO NOS SIRVE PARA DETECTAR EL SUELO
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(rayOrigin.position, Vector3.down * rayRange);
        }

        #endregion

        private void Crouch()
        {

        }

        private bool CrouchInput()
        {
            return Input.GetKey(KeyCode.LeftControl);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Damage")
            {
                GetComponent<IDamageable>().TakeDamage(5);
            }
        }

    }

}