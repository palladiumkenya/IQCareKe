using IQCare.Web.API.DependancyResolution;
using StructureMap;

namespace IQCare.Web.API {
    public static class StructuremapMvc {
        #region Public Properties

        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        #endregion
		
		#region Public Methods and Operators
		
		public static void End() {
            StructureMapDependencyScope.Dispose();
        }
		
        public static void Start() {
            IContainer container = IoC.Initialize();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            //DependencyResolver.SetResolver(StructureMapDependencyScope);
            //GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
            //DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }

        #endregion
    }
}