using DataLayer;
using EntityLayer;
using HaircuteUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenCvSharp;
using System.IO;
using System.Threading.Tasks;

namespace HaircuteUI.Controllers
{
    public class AiController : Controller
    {
        private readonly PhotoAnalysisService _photoAnalysisService;
        private readonly Context _dbContext;
        private readonly string _haarCascadePath = "wwwroot/Models/haarcascade_frontalface_default.xml";

        public AiController(Context context)
        {
            _photoAnalysisService = new PhotoAnalysisService();
            _dbContext = context;
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return Json(new { success = false, message = "No file uploaded." });

            // Save the photo
            var photoName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
            var photoPath = Path.Combine("wwwroot/uploads", photoName);
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            // Analyze photo
            var aiResult = ProcessPhotoWithAI(photoPath);

            // Save the suggestion
            var suggestion = new HairStyleSuggestion
            {
                UploadedPhotoPath = photoPath,
                SuggestedStyle = aiResult.Style,
                SuggestedColor = aiResult.Color
            };
            _dbContext.HairStyleSuggestions.Add(suggestion);
            _dbContext.SaveChanges();

            // Include face count in the response
            return Json(new
            {
                success = true,
                style = aiResult.Style,
                color = aiResult.Color,
                facesDetected = aiResult.FacesDetected,
                photoName
            });
        }

        private (string Style, string Color, int FacesDetected) ProcessPhotoWithAI(string photoPath)
        {
            // Load the image using OpenCV
            using var image = new Mat(photoPath, ImreadModes.Color);

            // Load HaarCascade for face detection
            using var faceCascade = new CascadeClassifier(_haarCascadePath);

            // Detect faces in the image
            var faces = faceCascade.DetectMultiScale(image, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(50, 50));

            // If no faces are detected
            if (faces.Length == 0)
            {
                return ("No Style Detected", "No Color Detected", 0);
            }

            // Analyze the largest detected face (or loop through all)
            var largestFace = faces.OrderByDescending(f => f.Width * f.Height).First();
            var faceRegion = new Mat(image, largestFace);

            // Placeholder for hairstyle/color suggestion logic
            string suggestedStyle = GenerateStyleSuggestion(largestFace);
            string suggestedColor = GenerateColorSuggestion();

            return (suggestedStyle, suggestedColor, faces.Length);
        }
        private string GenerateStyleSuggestion(Rect face)
        {
            if (face.Width > 150 && face.Height > 150)
            {
                return "Curly Layered Cut"; // For larger faces
            }
            else if (face.Width < 100 || face.Height < 100)
            {
                return "Short Pixie Cut"; // For smaller faces
            }
            return "Sleek Bob"; // For medium faces
        }

        private string GenerateColorSuggestion()
        {
            // Mock logic: You could analyze the image's color distribution here
            Random random = new Random();
            var colors = new[] { "Blonde", "Brown", "Black", "Ash Grey", "Golden Brown" };
            return colors[random.Next(colors.Length)];
        }
    }
}
