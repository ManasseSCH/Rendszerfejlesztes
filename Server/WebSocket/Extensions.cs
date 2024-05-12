using System.Reflection;

namespace Server
{
    public static class Extensions
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                        PathString path,
                                                        WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();//A ConnectionManager-t hozzáadjuk a szolgáltatásokhoz az életciklusától függően (Transient)

            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()))
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))//Ha a típusnak a bázistípusa a WebSocketHandler
                {
                    services.AddSingleton(type);//A WebSocketHandler-t hozzáadjuk a szolgáltatásokhoz az életciklusától függően (Singleton)
                }
            }

            return services;
        }
    }
}
