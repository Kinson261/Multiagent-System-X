// Copyright (C) 2018 Mercuna Developments Limited - All rights reserved
// This source file is part of the Mercuna Middleware
// Use, modification and distribution of this file or any portion thereof
// is only permitted as specified in your Mercuna License Agreement.
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Mercuna
{
    [RequireComponent(typeof(MercunaMoveController), typeof(Rigidbody))]
    [DefaultExecutionOrder(-102)]
    [AddComponentMenu("Mercuna 3D Navigation/Mercuna 3D Navigation")]
    public class Mercuna3DNavigation : MonoBehaviour
    {
        // These must match the enum in the native Mercuna library
        public enum MoveInForwardDirection { Always, Prefer, Independent};

        [Tooltip("Whether to restrict movement to facing direction")]
        public MoveInForwardDirection moveOnlyInLookDirection = MoveInForwardDirection.Prefer;

        [Tooltip("Whether to stop when reaching navigation destinations, or to keep moving through the end point")]
        public bool stopAtDestination = true;

        [Tooltip("Whether to avoid dynamic objects with a MercunaObstacle component while navigating")]
        public bool dynamicAvoidance = true;

        [Tooltip("Whether to use smooth paths, rather than simple polyline paths")]
        public bool smoothPaths = true;

        [Tooltip("Whether to prefer staying at the same height")]
        [Range(0.0f, 1.0f)]
        public float heightChangePenalty = 0.0f;

        [Tooltip("Determines what type of navigation modifier volumes this pawn is allowed to or is required to go through.")]
        public MerAgentUsageFlags usageFlags;

        public delegate void MoveCompleteDelegate(bool success);

        public void NavigateToLocation(Vector3 destination, MoveCompleteDelegate onMoveCompleteDelegate = null, float endDistance = 0.0f, float speedMultiple = 1.0f)
        {
            if (endDistance == 0.0f)
            {
                endDistance = GetRadius(gameObject);
            }
            IntPtr callbackPtr = RegisterCallback(onMoveCompleteDelegate);
            MoveToLocation(m_pNavigationComponent, destination, endDistance, speedMultiple, callbackPtr);
        }

        public void NavigateToLocations(Vector3[] destinations, MoveCompleteDelegate onMoveCompleteDelegate = null, float endDistance = 0.0f, float speedMultiple = 1.0f)
        {
            if (endDistance == 0.0f)
            {
                endDistance = GetRadius(gameObject);
            }
            IntPtr callbackPtr = RegisterCallback(onMoveCompleteDelegate);
            MerVector[] merTo = new MerVector[destinations.Length];
            for (int i = 0; i < destinations.Length; ++i)
            {
                merTo[i] = destinations[i];
            }
            MoveToLocations(m_pNavigationComponent, merTo.Length, merTo, endDistance, speedMultiple, callbackPtr);
        }

        // Add an additional location to the move request
        public bool NavigateToAdditionalLocation(Vector3 destination)
        {
            return AddDestinationLocation(m_pNavigationComponent, destination);
        }

        public bool UpdateNavigateToLocation(Vector3 destination)
        {
            return UpdateMoveToLocation(m_pNavigationComponent, destination);
        }

        public void NavigateToObject(GameObject destination, MoveCompleteDelegate onMoveCompleteDelegate = null, float endDistance = 0.0f, float speedMultiple = 1.0f)
        {
            if (endDistance == 0.0f)
            {
                endDistance = GetRadius(gameObject) + GetRadius(destination);
            }

            IntPtr destPtr = Mercuna.instance.GCCreateRef(destination);
            IntPtr callbackPtr = RegisterCallback(onMoveCompleteDelegate);
            MoveToEntity(m_pNavigationComponent, destPtr, endDistance, speedMultiple, callbackPtr);
            Mercuna.instance.GCReleaseRef(destPtr);
        }
        public void Track(GameObject destination, MoveCompleteDelegate onMoveCompleteDelegate = null, float distance = 0.0f, float speedMultiple = 1.0f)
        {
            if (distance == 0.0f)
            {
                distance = GetRadius(gameObject) * 3.0f + GetRadius(destination);
            }

            IntPtr destPtr = Mercuna.instance.GCCreateRef(destination);
            IntPtr callbackPtr = RegisterCallback(onMoveCompleteDelegate);
            TrackTarget(m_pNavigationComponent, destPtr, distance, speedMultiple, callbackPtr);
            Mercuna.instance.GCReleaseRef(destPtr);
        }

        public void Cancel()
        {
            CancelMove(m_pNavigationComponent);
        }

        // Re-read configuration from the Move Controller and Navigation Components and update the values in use.
        // Calling this cancels any current move commands.
        public void Reconfigure()
        {
            ConfigureInternal();
        }

        //---------------------------------------------------------------------

        [StructLayout(LayoutKind.Sequential)]
        public struct NavigationConfig
        {
            public float maxSpeed;
            public float maxForwardAccel;
            public float maxBackwardAccel;
            public float maxSidewaysAccel;
            public float maxAngSpeed;
            public float maxAngAccel;
            public float heightChangePenalty;

            [MarshalAs(UnmanagedType.I1)]
            public bool useAvoidance;
            [MarshalAs(UnmanagedType.I1)]
            public bool useSplineSteering;
            [MarshalAs(UnmanagedType.I1)]
            public bool stopAtDest;

            public uint moveOnlyInForwardDir;
            public uint requiredUsageFlags;
            public uint allowedUsageFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MoveOutput
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool bMoveIsAccel;
            public MerVector desiredMove;
            public MerVector desiredLookDir;
        }

        private IntPtr m_pNavigationComponent = IntPtr.Zero;
        private MercunaMoveController m_moveControl;
        private Rigidbody m_rigidBody;

        private float GetRadius(GameObject obj)
        {
            Vector3 center;
            float radius;
            Mercuna.GetCenterAndRadius(obj, obj.GetComponent<Collider>(), out center, out radius);
            return radius;
        }

        public float radius
        {
            get { return GetRadius(gameObject); }
        }

        private IntPtr RegisterCallback(MoveCompleteDelegate onMoveCompleteDelegate)
        {
            IntPtr callbackPtr;
            if (onMoveCompleteDelegate == null)
            {
                MoveCompleteDelegate dummyDelegate = (bool result) => { };
                callbackPtr = Mercuna.RegisterMoveCompleteDelegate(dummyDelegate);
            }
            else
            {
                callbackPtr = Mercuna.RegisterMoveCompleteDelegate(onMoveCompleteDelegate);
            }
            return callbackPtr;
        }

        private void OnEnable()
        {
            Mercuna.instance.EnsureInitialized();

            m_moveControl = GetComponent<MercunaMoveController>();
            if (m_moveControl == null)
            {
                Mercuna.Log(ELogLevel.Error, String.Format("No Mercuna Move Controller for %s", gameObject.name));
                return;
            }
            m_rigidBody = GetComponent<Rigidbody>();

            IntPtr entity = Mercuna.instance.GCCreateRef(gameObject);
            m_pNavigationComponent = CreateNavigationComponent(entity);
            Mercuna.instance.GCReleaseRef(entity);

            ConfigureInternal();
        }

        private void ConfigureInternal()
        {
            NavigationConfig config = new NavigationConfig();
            config.maxSpeed = m_moveControl.maxSpeed;
            config.maxForwardAccel = m_moveControl.maxForwardAccel;
            config.maxBackwardAccel = m_moveControl.maxBackwardAccel;
            config.maxSidewaysAccel = m_moveControl.maxSidewaysAccel;
            config.maxAngSpeed = m_moveControl.maxAngularSpeed * Mathf.Deg2Rad;
            config.maxAngAccel = m_moveControl.maxAngularAccel * Mathf.Deg2Rad;

            config.heightChangePenalty = heightChangePenalty;
            config.useAvoidance = dynamicAvoidance;
            config.useSplineSteering = smoothPaths;
            config.stopAtDest = stopAtDestination;

            config.moveOnlyInForwardDir = (uint)moveOnlyInLookDirection;
            config.requiredUsageFlags = usageFlags.requiredUsageFlags.GetPacked();
            config.allowedUsageFlags = usageFlags.allowedUsageFlags.GetPacked();

            ConfigureNavigation(m_pNavigationComponent, MercunaNavOctree.GetInstance().octreePtr, config);
        }

        private void OnDisable()
        {
            DestroyNavigationComponent(m_pNavigationComponent);
        }

        private void FixedUpdate()
        {
            if (m_pNavigationComponent == IntPtr.Zero)
            {
                Mercuna.Log(ELogLevel.Error, "Update before navigation component created");
                return;
            }

            MoveOutput move;
            ProcessMovement(m_pNavigationComponent, Time.fixedDeltaTime, out move);

            if (move.bMoveIsAccel)
            {
                m_moveControl.SetWorldAcceleration(move.desiredMove);
            }
            else
            {
                m_moveControl.SetDesiredWorldVelocity(move.desiredMove);
            }

            Vector3 lookDir = move.desiredLookDir;
            if (lookDir.magnitude > 1e-5f)
            {
                m_moveControl.SetDesiredWorldOrientation(Quaternion.LookRotation(lookDir.normalized));
            }
            else
            {
                if (moveOnlyInLookDirection==MoveInForwardDirection.Always && m_rigidBody.velocity.magnitude > 1e-3f)
                {
                    m_moveControl.SetDesiredWorldOrientation(Quaternion.LookRotation(m_rigidBody.velocity.normalized));
                }
                else
                {
                    m_moveControl.SetDesiredOrientationChange(Quaternion.identity);
                }
            }

            m_moveControl.Move();
        }

        // Calls to native
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern IntPtr CreateNavigationComponent(IntPtr entity);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void DestroyNavigationComponent(IntPtr pNavigation);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void ConfigureNavigation(IntPtr pNavigation, IntPtr pOctree, NavigationConfig config);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void ProcessMovement(IntPtr pNavigation, float deltaTime, out MoveOutput moveOutput);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void MoveToLocation(IntPtr pNavigation, MerVector destination, float endDistance, float speedMultiple, IntPtr callback);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void MoveToLocations(IntPtr pNavigation, int numDestinations, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] MerVector[] destinations, float endDistance, float speedMultiple, IntPtr callback);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern bool AddDestinationLocation(IntPtr pNavigation, MerVector destination);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void MoveToEntity(IntPtr pNavigation, IntPtr destination, float endDistance, float speedMultiple, IntPtr callback);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void TrackTarget(IntPtr pNavigation, IntPtr destination, float distance, float speedMultiple, IntPtr callback);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern void CancelMove(IntPtr pNavigation);
        [DllImport(Mercuna.MERCUNA_DLL_NAME, CallingConvention = Mercuna.MERCUNA_CALLING_CONVENTION)]
        static extern bool UpdateMoveToLocation(IntPtr pNavigation, MerVector destination);
    }
}
