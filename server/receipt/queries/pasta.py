import requests
import json

url = "http://127.0.0.1:5000/api/receipt"
data = {
    "name": "Pasta with Tomato Sauce",
    "description": "Delicious pasta with homemade tomato sauce.",
    "ingredients": {
        "Pasta": "250g",
        "Tomatoes": "4st",
        "Garlic": "2 cloves",
        'Olive Oil':'2 tbsp',
        'Salt and pepper':'to taste'
        },
    "cooking": """Cook pasta in salted water until done.
                    Chop tomatoes and garlic.
Heat olive oil in a pan, add garlic and tomatoes, simmer until soft.
Add cooked pasta to the tomato sauce, mix. Season with salt and pepper to taste""",
    "author": "Mario",
    'photo_url':'https://www.giallozafferano.com/images/228-22832/spaghetti-with-tomato-sauce_1200x800.jpg'
}

headers = {"Content-Type": "application/json"}
response = requests.post(url, json=data, headers=headers)

print(response.json())
