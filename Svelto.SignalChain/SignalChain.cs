#if UNITY_5 || UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Svelto.UI.Comms.SignalChain
{
    public class SignalChain
    {
        public SignalChain(Transform root)
        {
            _root = root;
            _behaviours = _root.GetComponents<MonoBehaviour>();
            _hierarchicalBehaviours = _root.GetComponentsInChildren<MonoBehaviour>(true);
        }

        public void Send<T>(T notification)
        {
            Send(typeof(T), notification);
        }

        public void Send<T>()
        {
            Send(typeof(T), null);
        }

        /// <summary>
        /// Send an event to all the components of the root. If a notification object 
        /// is passed, events must expect a value object.
        /// </summary>
        /// <param name="notificationType">Event is described by a type</param>
        /// <param name="notification">Event could be a value object</param>
        public void Send(Type notificationType, object notification)
        {
            for (var i = 0; i < _behaviours.Length; i++)
            {
                var behaviour = _behaviours[i];

                if (behaviour is IChainListener)
                    (behaviour as IChainListener).Listen(notification ?? notificationType);
            }
        }

        public void Broadcast<T>()
        {
            Broadcast(typeof(T), null);
        }

        public void Broadcast<T>(T notification)
        {
            Broadcast(typeof(T), notification, false);
        }

        /// <summary>
        /// Broadcast an event to all the components of the root and root children. 
        /// If a notification object is passed, events must expect a value object.
        /// </summary>
        /// <param name="notificationType">Event is described by a type</param>
        /// <param name="notification">Event could be a value object</param>
        public void Broadcast(Type notificationType, object notification)
        {
            Broadcast(notificationType, notification, false);
        }

        public void DeepBroadcast<T>()
        {
            DeepBroadcast(typeof(T), null);
        }

        public void DeepBroadcast<T>(T notification)
        {
            DeepBroadcast(typeof(T), notification);
        }

        /// <summary>
        /// Broadcast an event to all the components of the root and root children,
        /// even if the children are disabled. 
        /// If a notification object is passed, events must expect a value object.
        /// </summary>
        /// <param name="notificationType">Event is described by a type</param>
        /// <param name="notification">Event could be a value object</param>
        public void DeepBroadcast(Type notificationType, object notification)
        {
            Broadcast(notificationType, notification, true);
        }

        void Broadcast(Type notificationType, object notification, bool notifyDisabled)
        {
            for (var i = 0; i < _hierarchicalBehaviours.Length; i++)
            {
                var behaviour = _hierarchicalBehaviours[i];

                if (behaviour is IChainListener && (notifyDisabled == true || 
                        (notifyDisabled == false && behaviour.gameObject.activeInHierarchy == true)))
                    (behaviour as IChainListener).Listen(notification ?? notificationType);
            }
        }

        Transform       _root;
        MonoBehaviour[] _behaviours;
        MonoBehaviour[] _hierarchicalBehaviours;
    }
}
#endif