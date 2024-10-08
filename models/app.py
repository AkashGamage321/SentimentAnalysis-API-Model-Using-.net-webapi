from flask import Flask, request, jsonify
import tensorflow as tf
import numpy as np
from tensorflow.keras.preprocessing.text import Tokenizer
from tensorflow.keras.preprocessing.sequence import pad_sequences

# Initialize Flask app
app = Flask(__name__)

# Load the saved TensorFlow model
model = tf.keras.models.load_model('/workspaces/SentimentAnalysis-API-Model-Using-.net-webapi/models/sentiment_model.h5')

# Initialize tokenizer (should be the same as used during model training)
tokenizer = Tokenizer()
# Load your tokenizer's configuration or fit it on your training data if necessary
# tokenizer.fit_on_texts(training_texts) # Uncomment and fit if needed

max_length = 100  # Ensure this matches the max length used during training

# Define the prediction route
@app.route('/predict', methods=['POST'])
def predict():
    try:
        data = request.get_json()
        if not data or 'text' not in data:
            return jsonify({'error': 'Invalid input, please provide text'}), 400

        # Preprocess the input text
        input_sequence = tokenizer.texts_to_sequences([data['text']])
        input_padded = pad_sequences(input_sequence, maxlen=max_length)  # Ensure you use the same max_length

        # Predict sentiment
        prediction = model.predict(input_padded)

        # Determine sentiment label (modify as needed based on model output)
        sentiment = np.argmax(prediction)  # Assuming the model output is a probability distribution

        if sentiment == 0:
            sentiment_label = 'Negative'
        elif sentiment == 1:
            sentiment_label = 'Positive'


        # Return the sentiment as a JSON response
        return jsonify({'sentiment': sentiment_label, 'prediction': prediction.tolist()})  # Convert prediction to list for JSON response

    except Exception as e:
        print(f"Error occurred: {str(e)}")
        return jsonify({'error': 'Internal Server Error'}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
