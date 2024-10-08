using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentAnalysisApi.Models
{
    public class SentimentAnalysisModel
    {
        [Key]
        public int SentimentId { get;set; }
        public string Text { get;set; }
        // public decimal PredictionScore { get;set;}
        public string Sentiment { get;set; }
        // public DateTime CreatedDate { get;set; }
    }
}