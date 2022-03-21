// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Azure.Core;
using Azure.Identity;
using CommandLine;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModifyDevice
{
    public class Program
    {
        class Config
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string TenantId { get; set; }
            public string Hostname { get; set; }
            public string Deviceid { get; set; }
        }

        /// <summary>
        /// A sample to illustrate how to use Azure active directory for authentication to the IoT hub.
        /// <param name="args">Run with `--help` to see a list of required and optional parameters.</param>
        /// For more information on setting up AAD for IoT hub, see <see href="https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-dev-guide-azure-ad-rbac"/>
        /// </summary>
        public static async Task Main(string[] args)
        {
            var secretConf = new Config();

            // Parse application parameters
            Parameters parameters = null;
            ParserResult<Parameters> result = Parser.Default.ParseArguments<Parameters>(args)
                .WithParsed(parsedParams =>
                {
                    parameters = parsedParams;
                })
                .WithNotParsed(errors =>
                {
                    Environment.Exit(1);
                });

            // Initialize Azure active directory credentials.
            Console.WriteLine("Creating token credential.");

            // These environment variables are necessary for DefaultAzureCredential to use application Id and client secret to login.
            secretConf.ClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET")??""; 
            secretConf.ClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")??"";
            secretConf.TenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID")??"";
            secretConf.Hostname = Environment.GetEnvironmentVariable("AZURE_HOSTNAME")??"";
            secretConf.Deviceid = Environment.GetEnvironmentVariable("AZURE_DEVICE_ID")??"";

            // DefaultAzureCredential supports different authentication mechanisms and determines the appropriate credential type based of the environment it is executing in.
            // It attempts to use multiple credential types in an order until it finds a working credential.
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/azure.identity?view=azure-dotnet.
            TokenCredential tokenCredential = new DefaultAzureCredential();

            // There are constructors for all the other clients where you can pass AAD credentials - JobClient, RegistryManager, DigitalTwinClient
            using var registryManager = RegistryManager.Create(secretConf.Hostname, tokenCredential);
            using var serviceClient = ServiceClient.Create(secretConf.Hostname, tokenCredential, TransportType.Amqp);

            var setColorProgram = new ModifyColor(registryManager);
            await setColorProgram.ChangeColorProperties(serviceClient, secretConf.Deviceid);
        }
    }
}