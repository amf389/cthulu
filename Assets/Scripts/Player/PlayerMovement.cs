﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cthulu {
    public class PlayerMovement : UnityStandardAssets.Characters.FirstPerson.FirstPersonController {

        public Player player;

        protected override void GetInput(out float speed) {

            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
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

        public void SetSpeed(float speed) {
            m_WalkSpeed = speed;
        }

        public void UpdateMouseLock() {
            m_MouseLook.InternalLockUpdate();
        }

        public void Lock() {
            m_MouseLook.UnlockCursor();
            UpdateMouseLock();
        }

        /// <summary>
        /// Locking a cusor means it's invisible and doesn't move
        /// </summary>
        public void Unlock() {
            m_MouseLook.LockCursor();
            UpdateMouseLock();
        }

    }
}