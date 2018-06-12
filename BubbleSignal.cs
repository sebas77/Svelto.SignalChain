#if UNITY_5 || UNITY_5_3_OR_NEWER
using UnityEngine;

//TargetDispatch and Dispatch must be renabled

namespace Svelto.UI.Comms.SignalChain
{
    public class BubbleSignal<T>
    {
        SignalChain _signalChain;
                
        public BubbleSignal (Transform node)
        {
            Transform 	root = node;

            while (root != null && root.GetComponent<T>() == null)	
                root = root.parent;

            DesignByContract.Check.Assert(root != null, "Error: constructed a bubble signal where no root was found. this bubble signal will not function correctly");
            _signalChain = new SignalChain(root);
        }
        
        public void Dispatch<M>()
        {
            _signalChain.Broadcast<M>();
        }
        
        public void Dispatch<M>(M notification)
        {
            _signalChain.Broadcast(notification);
        }
        
        public void DeepDispatch<M>()
        {
            _signalChain.DeepBroadcast<M>();
        }
       
        public void DeepDispatch<M>(M notification)
        {
            _signalChain.DeepBroadcast(notification);
        }
        
        public void TargetedDispatch<M>()
        {
            _signalChain.Send<M>();
        }
        
        public void TargetedDispatch<M>(M notification)
        {
            _signalChain.Send(notification);
        }
    }
}
#endif