using Microsoft.ML;
using OpenCvSharp;
using System.IO;
namespace HaircuteUI.Models
{
    public class PhotoAnalysisService
    {
        private readonly string _haarCascadePath;

        public PhotoAnalysisService()
        {
            // Resolve the path to the Haarcascade XML file
            _haarCascadePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Models", "haarcascade_frontalface_default.xml");
            if (!File.Exists(_haarCascadePath))
            {
                throw new FileNotFoundException("Haarcascade file not found at path: " + _haarCascadePath);
            }
        }

        public (string Style, string Color) AnalyzePhoto(string photoPath)
        {
            // Ensure the photoPath exists
            if (!File.Exists(photoPath))
            {
                throw new FileNotFoundException("Photo file not found at path: " + photoPath);
            }

            try
            {
                // Load the image using OpenCV
                using var image = new Mat(photoPath, ImreadModes.Color);

                // Convert image to grayscale for better face detection
                using var grayImage = new Mat();
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

                // Load HaarCascade for face detection
                using var faceCascade = new CascadeClassifier(_haarCascadePath);

                // Detect faces in the image
                var faces = faceCascade.DetectMultiScale(
                    grayImage,
                    scaleFactor: 1.1,
                    minNeighbors: 5,
                    minSize: new OpenCvSharp.Size(30, 30)
                );

                // Mock AI Analysis (replace this with actual model predictions)
                string suggestedStyle = faces.Length > 0 ? "Layered Bob" : "Buzz Cut";
                string suggestedColor = faces.Length > 0 ? "Blonde" : "Black";

                return (suggestedStyle, suggestedColor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing the photo: " + ex.Message);
            }
        }
    }
}