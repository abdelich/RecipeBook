import requests
import json

url = "http://127.0.0.1:5000/api/receipt"

data = {
    "name": "Chicken Curry",
    "description": "A flavorful and aromatic chicken curry.",
    "ingredients": {
        "Chicken": "500g",
        "Onion": "1st",
        "Garlic": "3 cloves",
        'Ginger':'1 tsp',
        'Curry Powder':'2 tsp',
        'Coconut milk':'1 cup',
        'Rice':'1 cup'
        },
    "cooking": """Sauté sliced onion, garlic, and ginger in oil.
Add chicken pieces and brown.
Sprinkle with curry powder, stir.
Pour in coconut milk and simmer until cooked.
Serve with cooked rice.""",
    "author": "Rishi Sunak",
    'photo_url':'https://hips.hearstapps.com/del.h-cdn.co/assets/17/31/1501791674-delish-chicken-curry-horizontal.jpg?crop=0.665xw:0.998xh;0.139xw,0.00240xh&resize=1200:*'
}

headers = {"Content-Type": "application/json"}
response = requests.post(url, json=data, headers=headers)

print(response.json())
