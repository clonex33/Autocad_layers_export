# AutoCAD Layer Exporter

## Overview

The `LayerExporter` is an AutoCAD plugin designed to export information about layers in the current drawing to a text file. The exported data includes the layer names and the count of objects in each layer, along with metadata such as the current drawing file name, user name, and the date of export.

![image](https://github.com/user-attachments/assets/497019b5-b9f2-4495-8972-89d9a0834c88)
## Installation

1. Copy the `LayerExporter` class into your AutoCAD .NET project.
2. Make sure you reference the necessary AutoCAD namespaces and libraries:
   - `Autodesk.AutoCAD.Runtime`
   - `Autodesk.AutoCAD.ApplicationServices`
   - `Autodesk.AutoCAD.DatabaseServices`
   - `Autodesk.AutoCAD.EditorInput`
3. Build the project to create a DLL.
4. Load the DLL into AutoCAD using the `NETLOAD` command.

## Usage

1. Open a drawing in AutoCAD.
2. Run the command `ExportLayers` from the command line.

## What It Does

When the `ExportLayers` command is executed, the following steps are performed:

1. **Initialize Document and Editor**: 
   - The current AutoCAD document and editor are accessed.
   - The current drawing file name, user name, and current date are retrieved.

2. **Start Transaction**:
   - A transaction is started to safely interact with the AutoCAD database.

3. **Open Model Space**:
   - The current space (model space) is opened for reading.

4. **Count Layer Entities**:
   - The code iterates through all entities in the model space.
   - A dictionary is used to count the number of objects on each layer.

5. **Commit Transaction**:
   - The transaction is committed to finalize the read operations.

6. **Write to Text File**:
   - An output file named `LayerExport.txt` is created on the desktop.
   - The file contains the current drawing file name, user name, current date, and a list of layers with the count of objects on each layer.

7. **Notify User**:
   - A message is displayed in the AutoCAD command line indicating the location of the exported file.

## Example Output

![image](https://github.com/user-attachments/assets/67611d43-24b9-4414-b7c6-3bbbb432486b)


