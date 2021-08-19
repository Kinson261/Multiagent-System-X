// Copyright (C) 2018 Mercuna Developments Limited - All rights reserved
// This source file is part of the Mercuna Middleware
// Use, modification and distribution of this file or any portion thereof
// is only permitted as specified in your Mercuna License Agreement.
using System;
using UnityEngine;

namespace Mercuna
{
    /*
     * Move Controller expects that from FixedUpdate, a driving component will call:
     * - One of SetDesiredWorldVelocity, SetWorldAcceleration or SetLocalAcceleration, then
     * - One of SetDesiredOrientationChange, SetDesiredWorldOrientation or SetLocalAngularAcceleration, then
     * - Move
     */
    [RequireComponent(typeof(Rigidbody))]
    [DefaultExecutionOrder(-103)]
    [AddComponentMenu("Mercuna 3D Navigation/Mercuna Move Controller")]
    public class MercunaMoveController : MonoBehaviour
    {
        [Tooltip("Maximum speed")]
        public float maxSpeed = 5.0f;

        [Tooltip("Maximum acceleration in the forward direction")]
        public float maxForwardAccel = 3.0f;

        [Tooltip("Maximum acceleration in the backwards direction")]
        public float maxBackwardAccel = 3.0f;

        [Tooltip("Maximum acceleration in the horizontal and vertical directions")]
        public float maxSidewaysAccel = 1.5f;

        [Tooltip("Maximum angular speed in degrees/sec")]
        public float maxAngularSpeed = 90.0f;

        [Tooltip("Maximum angular acceleration in degrees/sec^2")]
        public float maxAngularAccel = 180.0f;

        // Cached reference to rigid body to move.
        private Rigidbody m_rigidBody;

        private Vector3 m_acceleration = new Vector3(0, 0, 0);
        private Vector3 m_angAccel = new Vector3(0, 0, 0);

        // Use this for initialization
        private void Start()
        {
            m_rigidBody = GetComponent<Rigidbody>();

            if (m_rigidBody == null)
            {
                Mercuna.Log(ELogLevel.Error, String.Format("No RigidBody for %s", gameObject.name));
                return;
            }

            m_rigidBody.maxAngularVelocity = Mathf.Deg2Rad * maxAngularSpeed;
        }

        public void SetDesiredWorldVelocity(Vector3 desiredVel)
        {
            Vector3 curVelLocal = m_rigidBody.transform.InverseTransformDirection(m_rigidBody.velocity);
            Vector3 desiredVelLocal = m_rigidBody.transform.InverseTransformDirection(desiredVel);

            SetLocalAcceleration((desiredVelLocal - curVelLocal) * 10.0f);
        }

        public void SetWorldAcceleration(Vector3 desiredAccel)
        {
            Vector3 desiredAccelLocal = m_rigidBody.transform.InverseTransformDirection(desiredAccel);
            SetLocalAcceleration(desiredAccelLocal);
        }

        public void SetLocalAcceleration(Vector3 desiredAccelLocal)
        {
            m_acceleration[0] = Mathf.Clamp(desiredAccelLocal[0], -maxSidewaysAccel, maxSidewaysAccel);
            m_acceleration[1] = Mathf.Clamp(desiredAccelLocal[1], -maxSidewaysAccel, maxSidewaysAccel);
            m_acceleration[2] = Mathf.Clamp(desiredAccelLocal[2], -maxBackwardAccel, maxForwardAccel);
        }

        public void SetDesiredOrientationChange(Quaternion orientChange)
        {
            float angle;
            Vector3 axis;
            orientChange.ToAngleAxis(out angle, out axis);

            if (angle < 0.0f) angle += 360.0f;

            if (angle > 180.0f)
            {
                angle = 360.0f - angle;
                axis = -axis;
            }

            Vector3 angVelLocal = Mathf.Rad2Deg * m_rigidBody.transform.InverseTransformDirection(m_rigidBody.angularVelocity);

            if (angle < 1e-2)
            {
                SetLocalAngularAcceleration(-angVelLocal * 4.0f);
                return;
            }
            
            Vector3 axisLocal = m_rigidBody.transform.InverseTransformDirection(axis);

            float desiredAngularSpeed = Mathf.Min(Mathf.Sqrt(2.0f * angle * maxAngularAccel), maxAngularSpeed);
            Vector3 desiredAngVelLocal = axisLocal * desiredAngularSpeed;

            SetLocalAngularAcceleration((desiredAngVelLocal - angVelLocal) / Time.fixedDeltaTime);
        }

        public void SetDesiredWorldOrientation(Quaternion desiredOrientation)
        {
            SetDesiredOrientationChange(desiredOrientation * Quaternion.Inverse(m_rigidBody.rotation));
        }

        public void SetLocalAngularAcceleration(Vector3 desiredAccelLocal)
        {
            if (desiredAccelLocal.magnitude > maxAngularAccel)
            {
                desiredAccelLocal *= maxAngularAccel / desiredAccelLocal.magnitude;
            }
            m_angAccel = desiredAccelLocal;
        }

        // Called to move the GameObject.
        public void Move()
        {
            m_rigidBody.AddRelativeForce(m_acceleration, ForceMode.Acceleration);
            m_rigidBody.AddRelativeTorque(m_angAccel * Mathf.Deg2Rad, ForceMode.Acceleration);
        }

        static private float StoppingDistance(float velocity, float acceleration)
        {
            return velocity * velocity / (2.0f * acceleration);
        }
    }

}
