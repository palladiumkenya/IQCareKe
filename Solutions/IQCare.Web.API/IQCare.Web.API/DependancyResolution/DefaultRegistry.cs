//using DataAccess.WebApi;
using IQCare.WebApi.Logic.MessageHandler;
using StructureMap;

namespace IQCare.Web.Api.DependancyResolution {
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.Assembly("IQCare.Web.Api");
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            //For<ApiContext>().Use<ApiContext>()
            //    .SelectConstructor(() => new ApiContext());

            // section for registring services

            For<IOutgoingMessageService>()
                .Use<OutgoingMessageService>();

            For<IIncomingMessageService>()
                .Use<IncomingMessageService>();

        }

        #endregion
    }
}