using System;

namespace Svelto.UI.Comms.SignalChain
{
	public interface IChainListener
	{
		void Listen(object message);
	}
}

