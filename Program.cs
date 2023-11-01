using System.Diagnostics;

namespace CsProfHomeWork7
{
    public class Program
    {
        static void Main(string[] args)
        {
            MeasureExecutionTime(".");
        }
        public static int CountSpacesInFile(string filePath)
        {
            var content = File.ReadAllText(filePath);
            return content.Length - content.Replace(" ", "").Length;
        }

        public static int CountSpacesInDirectory(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath, "*.txt");
            var tasks = files.Select(filePath => Task.Run(() => CountSpacesInFile(filePath))).ToArray();

            Task.WaitAll(tasks);
            return tasks.Sum(t => t.Result);
        }

        public static void MeasureExecutionTime(string directoryPath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var totalSpaces = CountSpacesInDirectory(directoryPath);

            stopwatch.Stop();
            Console.WriteLine($"Total spaces: {totalSpaces}. Time taken: {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}