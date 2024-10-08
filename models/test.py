from tensorflow.keras.models import load_model

# Load the model
model = load_model('/workspaces/SentimentAnalysis-API-Model-Using-.net-webapi/models/sentiment_model.h5')  # Ensure the path is correct
# Sample input
sample_text = "I love this product!"

# Ensure model is loaded correctly
prediction = model.predict(sample_text)
print(f"Prediction: {prediction}")
