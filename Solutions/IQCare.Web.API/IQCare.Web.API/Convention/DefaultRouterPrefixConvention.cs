using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace IQCare.Web.Api.Convention
{
    public class DefaultRouterPrefixConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var applicationController in application.Controllers)
            {
                applicationController.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = "[controller]"
                    }
                });
            }
        }
    }
}
