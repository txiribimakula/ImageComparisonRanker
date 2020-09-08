namespace ImageComparisonRanker
{
    using ImageMagick;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args) {
            DirectoryInfo d = new DirectoryInfo(@".\");
            FileInfo[] Files = d.GetFiles("*.png");

            MagickImage referenceImage = new MagickImage(args[0]);
            referenceImage.Compose = CompositeOperator.Difference;
            referenceImage.ColorFuzz = new Percentage(5);

            Dictionary<string, double> imageComparisons = new Dictionary<string, double>();

            foreach (FileInfo file in Files) {
                MagickImage image = new MagickImage(file.FullName);
                double diff = referenceImage.Compare(image, ErrorMetric.Absolute);
                imageComparisons.Add(file.Name, diff);
            }

            IOrderedEnumerable<KeyValuePair<string, double>> orderedImageComparisons = imageComparisons.OrderBy(item => item.Value);

            foreach (var item in orderedImageComparisons) {
                Console.Write(item.Key);
                Console.Write(" - ");
                Console.WriteLine(item.Value);
            }

            Console.ReadLine();
        }
    }
}
