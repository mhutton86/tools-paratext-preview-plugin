using PpmMain.Models;
using System;
using System.IO;
using System.Reflection;

namespace TptPublish
{
    class Program
    {
        const string PluginManifestFilename = "TypesettingPreviewPlugin.json";
        const string PluginLibraryFilename = "TptMain.dll";
        static readonly string SolutionRootPath = Path.Combine("..", "..", "..", "..");
        static readonly string PluginManifestPath = Path.Combine(SolutionRootPath, PluginManifestFilename);
        static readonly string PluginLibraryPath = Path.Combine(SolutionRootPath, "bin", "Release", PluginLibraryFilename);

        static void Main(string[] args)
        {
            // TODO pull in our plugin's PPM manifest
            var pluginDescription = PluginDescription.FromFile(PluginManifestPath);

            // TODO pull the plugin's version from the TptMain version
            var assembly = Assembly.LoadFrom(PluginLibraryPath);
            var version = assembly.GetName().Version;
            Console.WriteLine($"Previous version: '{pluginDescription.Version}'");
            Console.WriteLine($"Plugin DLL version: '{version}'");

            // TODO provide a review of the plugin manifest
            Console.WriteLine("Please review the plugin description");
            Console.WriteLine(pluginDescription.ToString());

            // TODO put the plugin and manifest in a temporary location for deployment
            var tempPreDeployDirectoryPath = Path.Combine(Path.GetTempPath(), $"PPM Release v{version} - {Path.GetRandomFileName()}");
            Console.WriteLine($"Creating the following temporary pre-deployment directory");
            var tempPreDeployDirectoryInfo = Directory.CreateDirectory(tempPreDeployDirectoryPath);

            // TODO transfer the plugin and manifest to the PPM repo
            File.Copy(PluginManifestPath, Path.Combine(tempPreDeployDirectoryInfo.FullName, PluginManifestFilename));
            File.Copy(PluginLibraryPath, Path.Combine(tempPreDeployDirectoryInfo.FullName, PluginLibraryFilename));

            // TODO rename the plugin and manifest

            // TODO deploy the plugin and manifest to the PPM repository
            // Note: We presume the deployer has set up their AWS credentials.

            // TODO review the AWS user and region we're about to use


            // TODO clean up temporary resources
            //Directory.Delete(tempPreDeployDirectoryInfo.FullName, true);
        }
    }
}
