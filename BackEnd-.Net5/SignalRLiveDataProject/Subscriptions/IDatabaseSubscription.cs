using System;
namespace SignalRLiveDataProject.Subscriptions
{
	public interface IDatabaseSubscription
	{
		void Configure(string tableName);
	}
}

