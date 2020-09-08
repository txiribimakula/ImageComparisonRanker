namespace ImageComparisonRanker
{
    using ImageMagick;

    class Program
    {
        static void Main(string[] args) {
            var image1Path = @".\cat1.png";
            var image2Path = @".\cat1.png";
            var image3Path = @".\catDiff.png";

            MagickImage image1 = new MagickImage(image1Path);
            image1.Compose = CompositeOperator.Difference;
            image1.ColorFuzz = new Percentage(5);
            MagickImage image2 = new MagickImage(image2Path);
            image2.Compose = CompositeOperator.Difference;

            MagickImage diffImage = new MagickImage();

            double diff = image1.Compare(image2, ErrorMetric.Absolute, diffImage);

            diffImage.Write(image3Path);
        }
    }
}
