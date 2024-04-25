namespace RealTimeDB.Subscription.Middleware;
public static class DatabaseSubscriptionMiddleware
{
    public static void UseDatabaseSubscription<T>(this IApplicationBuilder builder, string tableName) where T : class, IDataBaseSubscription
    {
        T? subscription = builder.ApplicationServices.GetService(typeof(T)) as T;
        subscription.Configure(tableName);

    }
}
