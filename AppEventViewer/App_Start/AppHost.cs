using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Mvc;
using AppEventViewer.Repository;
using AppEventViewer.ServiceInterface;
using ServiceStack.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppEventViewer.App_Start.AppHost), "Start")]

//IMPORTANT: Add the line below to MvcApplication.RegisterRoutes(RouteCollection) in the Global.asax:
//routes.IgnoreRoute("api/{*pathInfo}"); 

/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace AppEventViewer.App_Start
{
	//A customizeable typed UserSession that can be extended with your own properties
	//To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>
	public class CustomUserSession : AuthUserSession
	{
		public string CustomProperty { get; set; }
	}

	public class AppHost
		: AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Events from windows server nodes", typeof(EventService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
		
			//Configure User Defined REST Paths
            //Routes
            //  .Add<Hello>("/hello")
            //  .Add<Hello>("/hello/{Name*}")
              //.Add<Events>("/Events/")
              //.Add<Events>("/Events/{Lag*}");

			//Uncomment to change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//});

			//Enable Authentication
			//ConfigureAuth(container);

              var appSettings = new AppSettings();

              string baseApiUrl = appSettings.Get("BaseApiUrl","foo");
			//Register all your dependencies
//            container.Register<IJellybeanDispenser>(c => new VanillaJellybeanDispenser());
//            container.Register(c => new SweetVendingMachine(c.Resolve<IJellybeanDispenser>()));
//            container.Register(c => new SweetShop(c.Resolve<SweetVendingMachine>()));
// See more at: http://anthonysteele.co.uk/the-funq-ioc-container#sthash.ketDSF72.dpuf
            container.Register<IAppConfig>(c=>new AppConfig());
            container.Register<IEventRepository>(c=> new EventRepository());
		    container.Resolve<IEventRepository>().Config = container.Resolve<IAppConfig>();
           // container.Register(c => new TodoRepository());

            //Register a external dependency-free 
            container.Register<ICacheClient>(new MemoryCacheClient());
            //Configure an alt. distributed persistent cache that survives AppDomain restarts. e.g Redis
            //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager("localhost:6379"));

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

		/* Uncomment to enable ServiceStack Authentication and CustomUserSession
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new CustomUserSession(),
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
					new FacebookAuthProvider(appSettings), 
					new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
			authRepo.CreateMissingTables();
		}
		*/

	    private void ConfigureAuth(Funq.Container container)
	    {
	        var appSettings = new AppSettings();
            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(),
                new IAuthProvider[] {
					new BasicAuthProvider(appSettings), 
				}));
	        var userRepository = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(userRepository);
	        string hash;
	        string salt;
            new SaltedHash().GetHashAndSaltString("password",out hash,out salt);
	        userRepository.CreateUserAuth(new UserAuth
	            {
	                Id = 1 ,
	            DisplayName = "JoeUser",
	                Email = "joe@user.com",
	                UserName = "jUser",
	                FirstName = "Joe",
	                LastName = "User",
	                PasswordHash = hash,
	                Salt = salt

	            },"password");
	    }

	    public static void Start()
		{

			new AppHost().Init();
		}
	}
}