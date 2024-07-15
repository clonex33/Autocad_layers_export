using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;
using System.Collections.Generic;

[assembly: CommandClass(typeof(Autocad_layers_export.LayerExporter))]

namespace Autocad_layers_export
{
    public class LayerExporter
    {
        [CommandMethod("ExportLayers")]
        public void ExportLayers()
        {
            // Get the current document
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            // Get the current drawing file name
            string currentFileName = doc.Name;

            // Get the user name
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // Get the current date
            string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd");

            // Start a transaction
            using (Transaction trans = doc.Database.TransactionManager.StartTransaction())
            {
                // Open the current space (model space)
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForRead);

                // Dictionary to hold layer names and object counts
                Dictionary<string, int> layerCounts = new Dictionary<string, int>();

                // Iterate through the entities in the block table record
                foreach (ObjectId objId in btr)
                {
                    Entity entity = (Entity)trans.GetObject(objId, OpenMode.ForRead);
                    string layerName = entity.Layer;

                    if (layerCounts.ContainsKey(layerName))
                    {
                        layerCounts[layerName]++;
                    }
                    else
                    {
                        layerCounts[layerName] = 1;
                    }
                }

                // Commit the transaction
                trans.Commit();

                // Specify the output file path (to the desktop)
                string outputFilePath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                    "LayerExport.txt");

                // Write header, current file name, user name, current date, and layer names and counts to a text file
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    writer.WriteLine($"Current File: {currentFileName}");
                    writer.WriteLine($"User Name: {userName}");
                    writer.WriteLine($"Date: {currentDate}");
                    writer.WriteLine();
                    writer.WriteLine("Layer Name\t\tNumber of Objects");
                    writer.WriteLine("-------------------------------");

                    foreach (var entry in layerCounts)
                    {
                        writer.WriteLine($"{entry.Key,-30}\t{entry.Value}");
                    }
                }

                // Inform the user
                ed.WriteMessage($"\nLayer data exported to {outputFilePath}");
            }
        }
    }
}
