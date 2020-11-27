using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city.api.Context
{
    public class ExternalAccessSettings
    {
        private readonly string _session;
        private readonly IConfiguration _configuration;

        public Uri AuthenticateApi { get { return new Uri(this._configuration.ReadConfig<string>(this._session, nameof(AuthenticateApi))); } }

        public ExternalAccessSettings(string session, IConfiguration configuration)
        {
            this._session = session;
            this._configuration = configuration;
        }
    }
}
