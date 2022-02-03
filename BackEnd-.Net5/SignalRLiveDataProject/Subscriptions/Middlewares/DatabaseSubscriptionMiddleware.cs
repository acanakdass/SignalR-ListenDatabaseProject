using System;
using Microsoft.AspNetCore.Builder;

namespace SignalRLiveDataProject.Subscriptions.Middlewares
{
	public  static class DatabaseSubscriptionMiddleware
	{
		public static void UseDatabaseSubscription<T>(this IApplicationBuilder builder,string tableName) where T:class,IDatabaseSubscription
        {
			var subscription = (T)builder.ApplicationServices.GetService(typeof(T));
			subscription.Configure(tableName);
        }
	}
}

