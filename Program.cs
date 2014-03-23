using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HallOfFameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFile = "HallOfFame.txt";
            string input;

            using (var reader = new FileInfo(inputFile).OpenText())
            {
                input = reader.ReadToEnd();
            }

            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var data = lines.Select(line => new LineWithDate
                                                {
                                                    Line = line,
                                                    Date = DateTime.Parse(line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0])
                                                }).ToList();

            var output = new StringBuilder();

            foreach (var datum in data.OrderByDescending(i => i.Date))
            {
                output.AppendLine(datum.Line);
            }

            var parts = inputFile.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            using (var writer = new FileInfo(parts[0] + "_formatted." + parts[1]).CreateText())
            {
                writer.Write(output.ToString().Trim());
            }
        }
    }

    class LineWithDate
    {
        public string Line { get; set; }
        public DateTime Date { get; set; }
    }
}
