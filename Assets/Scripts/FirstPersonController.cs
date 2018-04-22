using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson {
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour {

        [SerializeField] private bool m_IsWalking;
        [SerializeField] private bool m_IsCrouching; //NEW
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float m_CrouchSpeed; //NEW
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        public float m_Height = 1.8f;
        public float m_CrouchHeight = .5f;

        private bool paused = false;

        // Use this for initialization
        private void Start() {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_AudioSource = GetComponents<AudioSource>()[0];
            m_MouseLook.Init(transform, m_Camera.transform);
        }


        // Update is called once per frame
        private void Update() {
            if(!paused)
                RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump && !m_IsCrouching) {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded) {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded) {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound() {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate() {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            if (m_CharacterController.isGrounded) {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump) {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();

            Crouch();
        }


        private void PlayJumpSound() {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed) {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0)) {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep)) {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio() {
            if (!m_CharacterController.isGrounded) {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed) {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob) {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded) {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed) {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);

            //Crouching works by pressing and holding the left control key or toggling via the c key.
            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                m_IsCrouching = true;
            } else if (Input.GetKeyUp(KeyCode.LeftControl)) {
                m_IsCrouching = false;
            } else if (Input.GetButtonDown("Crouch")) {
                m_IsCrouching = !m_IsCrouching;
            }
#endif
            // set the desired speed to be walking or running or crouch
            if (m_IsCrouching) {
                speed = m_IsWalking ? m_CrouchSpeed : m_WalkSpeed; //Crouch walking or crouch running
            }
            else {
                speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed; //Walking or running
            }

            //speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1) {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0) {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        public void SetPause() {
            paused = !paused;
        }

        private void RotateView() {
            if (!paused) {
                m_MouseLook.LookRotation(transform, m_Camera.transform);
            }
        }


        private void OnControllerColliderHit(ControllerColliderHit hit) {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below) {
                return;
            }

            if (body == null || body.isKinematic) {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }


        private float camStandHeight = .8f, camCrouchHeight = .4f; //This is the local height that the camera will be at when standing or crouching

        private float standVel = 2.5f, crouchVel = 2.5f;
        private float standSmT = 0.5f, crouchSmT = 0.5f;

        //NEW
        private void Crouch() {
			float crouchspeed = 5f;
            float heightChange = .5f * Time.fixedDeltaTime * 8;
            float deltaHeight = m_Height - m_CharacterController.height;
            if (m_IsCrouching && m_CharacterController.height >= m_CrouchHeight) {
                //This changes the Camera height down
                Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, camCrouchHeight), heightChange);
                //This moves the controller's collider's height downwards
				if (Mathf.SmoothDamp(m_CharacterController.height, m_CrouchHeight, ref crouchVel, crouchSmT, crouchspeed) < m_CrouchHeight) { //base case
                    m_CharacterController.height = m_CrouchHeight;
                } else {
                    //Smooth crouch down
					m_CharacterController.height = Mathf.SmoothDamp(m_CharacterController.height, m_CrouchHeight, ref crouchVel, crouchSmT, crouchspeed);
                }
            } else if (!m_IsCrouching && m_CharacterController.height < m_Height) { //Standing up, making sure not to go over. Then check if there's anything above.
                //This changes the Camera height up
                if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.up, camStandHeight - Camera.main.transform.localPosition.y))
                    Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, camStandHeight), heightChange);
                //This moves the controller's collider's height upwards
                if (!Physics.Raycast(m_CharacterController.transform.TransformPoint(0, m_CharacterController.height, 0), m_CharacterController.transform.up, deltaHeight))
				if (Mathf.SmoothDamp(m_CharacterController.height, m_Height, ref standVel, standSmT, crouchspeed) > m_Height) {//base case
                        m_CharacterController.height = m_Height;
                } else {
                    float beforeHeight = m_CharacterController.height;
					m_CharacterController.height = Mathf.SmoothDamp(m_CharacterController.height, m_Height, ref standVel, standSmT, crouchspeed);
                    m_CharacterController.transform.position += new Vector3(0, m_CharacterController.height - beforeHeight, 0);
                }

            }

            if (m_IsCrouching) {
                m_AudioSource.volume = .313f/2;
            } else if (!m_IsCrouching && m_AudioSource.volume != .313f) {
                m_AudioSource.volume = .313f;
            }
        }

        public bool IsCrouched() {
            return m_IsCrouching;
        }
    }
}