using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SentimentAnalysisApi.Data;
using SentimentAnalysisApi.Models;

namespace SentimentAnalysisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentController:ControllerBase
    {
        private readonly ApplicationDbContext _context ;
        private readonly HttpClient _httpClient ;
        public SentimentController(ApplicationDbContext context , HttpClient httpClient)
        {
            _context =context;
            _httpClient = httpClient;
            
        }
        // [HttpPost("predict")]
        // public async Task<IActionResult> Predict([FromBody] string inputText)
        // {
        //     var requestPayload = new { text = inputText };
        //     var jsonPlayload = JsonConvert.SerializeObject(requestPayload);
        //     var content = new StringContent(jsonPlayload , Encoding.UTF8 , "application/json");

        //     var response = await _httpClient.PostAsync("https://improved-space-fiesta-g45p6pj447ggcwq77-5000.app.github.dev/predict", content);
        //     var jsonResponse = await response.Content.ReadAsStringAsync();

        //     // Parse the response from Flask
        //     var result = JsonConvert.DeserializeObject<PredictionResult>(jsonResponse);
            
        //     // Save the result to the database
        //     var sentimentEntry = new SentimentAnalysisModel
        //     {
        //         Text = inputText,
        //         Sentiment = result.Sentiment,
        //     };

        //     _context.SentimentAnalyses.Add(sentimentEntry);
        //     await _context.SaveChangesAsync();

        //     return Ok(new { result.Sentiment, result.Prediction });
        // }

        // private class PredictionResult
        // {
        //     public string Sentiment { get; set; }
        //     public float Prediction { get; set; }
        // }
    [HttpPost("predict")]
    public async Task<IActionResult> Predict([FromBody] string inputText)
    {
        try
        {
            // Call the Flask API
            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/predict",
                new StringContent(JsonConvert.SerializeObject(new { text = inputText }), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error calling sentiment analysis service.");
            }

            var result = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(result);
            var sentiment = jsonResponse.sentiment;
            var prediction = jsonResponse.prediction;

            // Save the result to the database
            var sentimentEntry = new SentimentAnalysisModel
            {
                Text = inputText,
                Sentiment = sentiment,
                // PredictionScore = prediction // You may need to adjust this based on your model output
            };

            _context.SentimentAnalyses.Add(sentimentEntry);
            await _context.SaveChangesAsync();

            return Ok(new { sentiment, prediction });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    }
}