import requests
import json

url = "http://127.0.0.1:5000/api/receipt"
data = {
    "name": "Greek Salad",
    "description": "Fresh and healthy Greek salad.",
    "ingredients": {
        "Tomatoes": "2st",
        "Cucumbers": "1st",
        "Red Onion": "1st",
        'Red Peppers':'1st',
        'Kalamata Olives':'1/2 cup',
        'Feta Cheese':'100g',
        'Olive Oil':'3 tbps',
        'Salt and pepper': 'to taste'
        },
    "cooking": """Dice all vegetables.
Add olives and chopped feta cheese.
Drizzle with olive oil, season with salt and pepper. Mix well.""",
    "author": "Platon",
    'photo_url':'https://www.wellplated.com/wp-content/uploads/2022/05/Greek-Salad-Recipe-Easy.jpg'
}

headers = {"Content-Type": "application/json"}
response = requests.post(url, json=data, headers=headers)

print(response.json())
