using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fraxiinus.Rofl.Extract.Data.Models;
using Fraxiinus.Rofl.Extract.Data.Readers;

class RoflBatchExporter
{
    public static async Task Main(string[] args)
    {
        // Directory containing .rofl files
        string inputDirectory = @"test_replays";
        // Output directory for JSON files
        string outputDirectory = @"test_jsons";

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var roflFiles = Directory.GetFiles(inputDirectory, "*.rofl");
        var options = new ReplayReaderOptions { LoadPayload = true }; // Customize options as needed

        foreach (var roflFile in roflFiles)
        {
            try
            {
                // Load the .rofl file
                ROFL replay = await RoflReader.LoadAsync(roflFile, options);
                
                // Define the output file path
                string outputFilePath = Path.Combine(outputDirectory, 
                    Path.GetFileNameWithoutExtension(roflFile) + ".json");
                
                // Convert and save as JSON
                await replay.ToJsonFile(new FileInfo(roflFile), outputFilePath);
                Console.WriteLine($"Successfully converted: {roflFile} to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {roflFile}: {ex.Message}");
            }
        }
    }
}
