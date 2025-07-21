using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace UnitTestsProject.Tests.Components
{
    internal class ConfigurationMock : Microsoft.Extensions.Configuration.IConfiguration
    {
        public string this[string key] { get => GetSectionString(key); set => throw new NotImplementedException(); }



        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return null;
        }

        public IChangeToken GetReloadToken()
        {
            return null;
        }

        public string GetSectionString(string key)
        {
            return key switch
            {
                "ConfiguracionAuditoria:CodigoAplicacion" => "PLANEACIONCAPTURAPPTO",
                _ => "PLANEACIONCAPTURAPPTO",
            };
        }
        public IConfigurationSection GetSection(string key)
        {
            IConfigurationSection configurationSection = null;
            switch (key)
            {
                case "ConfiguracionNotificaciones:Url":
                    dynamic rest = new { Key = "xxx", Value = "www.google.com", Path = "" };
                    return (IConfigurationSection)rest;

                case "ConfiguracionNotificaciones:SubscriptionKey":
                    dynamic rest2 = new { Key = "zzz", Value = "989898656565797887897", Path = "" };
                    return (IConfigurationSection)rest2;

                default:
                    return configurationSection;
            }
        }
    }
}
