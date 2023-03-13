using NAudio.Wave;

Console.WriteLine("Input Folder Path");
string? inputFolderPath = Console.ReadLine();

Console.WriteLine("Output Folder Path");
string? outputFolderPath = Console.ReadLine();

if (!Directory.Exists(inputFolderPath))
{
    Console.WriteLine("Please specify a valid input folder!");
    return;
}

if (!Directory.Exists(outputFolderPath))
{
    Console.WriteLine("Please specify a valid output folder!");
    return;
}

var inputFiles = Directory.GetFiles(inputFolderPath, "*.m4a", SearchOption.AllDirectories)
        .Concat(Directory.GetFiles(inputFolderPath, "*.mkv", SearchOption.AllDirectories))
        .ToArray();

if (inputFiles.Length == 0)
{
    Console.WriteLine(".Mkv or .M4a file found in folder!");
    return;
}

Console.WriteLine($"A total of  {inputFiles.Length} valid files were found.");

int count = 0;
foreach (var inputFile in inputFiles)
{
    var fileName = Path.GetFileNameWithoutExtension(inputFile);
    var outputPath = Path.Combine(outputFolderPath, fileName + ".mp3");

    using (var reader = new MediaFoundationReader(inputFile))
    {
        MediaFoundationEncoder.EncodeToMp3(reader, outputPath);
    }

    // Rename the file
    // File.Move(outputPath, Path.Combine(outputFolderPath , fileName + "-volkan" + ".mp3"));

    count++;
    Console.WriteLine($"File {inputFile} Converted. ({count}/{inputFiles.Length})");
}

Console.WriteLine($"{count} files converted.");

Console.ReadKey();