// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using StructureMap;
//using IQ.ApiLogic.Infrastructure.Context;

namespace IQCare.Web.API.DependancyResolution {
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.Assembly("IQ.ApiLogic");
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            //For<ApiContext>().Use<ApiContext>()
            //    .SelectConstructor(() => new ApiContext());

            // section for registring services


            //For<IDashboardService>()
            //    .Use<DashboardService>().Ctor<string>("baseUrl").Is(Properties.Settings.Default.ServerAPI);


            // AdminService(string baseUrl, string adminUser, string adminPassword)

            //For<IMessageService>().Use<MessageService>()
            //    .Ctor<string>("baseUrl").Is(Properties.Settings.Default.ServerAPI)
            //    .Ctor<string>("adminUser").Is(Properties.Settings.Default.APIAdminUser)
            //    .Ctor<string>("adminPassword").Is(Properties.Settings.Default.APIAdminPassword);


        }

        #endregion
    }
}