import requests
import json

url = "http://127.0.0.1:5000/api/receipt"
data = {
    "name": "Borscht",
    "description": "Borscht is a hearty and vibrant soup originating from Eastern Europe, particularly popular in Ukrainian, Russian, and Polish cuisines. Its distinctive deep red color comes from beets, a key ingredient in this flavorful dish. Borscht typically combines a variety of vegetables such as carrots, potatoes, cabbage, tomatoes, and bell peppers, creating a rich and wholesome flavor profile.",
    "ingredients": {"beets": "2st", "cabbage": "1st", "potatoes": "2st", 'carrots':'2st', 'onions':'2st'},
    "cooking": """1 Prepare Vegetables:

Peel and grate the beets and carrots.
Finely chop the onion and garlic.
Dice the potatoes.
Shred the cabbage.
Chop the tomatoes and bell pepper.
2 Sauté Vegetables:

In a large pot, heat the vegetable oil over medium heat.
Add the chopped onion and sauté until translucent.
Add the grated beets and carrots. Cook for about 5-7 minutes until the vegetables start to soften.
3 Add Tomatoes and Paste:

Stir in the chopped tomatoes and tomato paste. Cook for an additional 5 minutes.
4 Add Broth and Simmer:

Pour in the broth and add the diced potatoes, shredded cabbage, and bay leaf.
Bring the mixture to a boil, then reduce the heat to low and let it simmer for about 15-20 minutes or until the vegetables are tender.
5 Season:

Add sugar, salt, and pepper to taste. Adjust the seasoning as needed.
7 Finish Cooking:

Add the chopped bell pepper and minced garlic. Continue to simmer for an additional 5-7 minutes.
Serve:

Remove the bay leaf.
Ladle the borscht into bowls.
7 Serve hot with a dollop of sour cream and a sprinkle of fresh dill on top.""",
    "author": "Zelenskiy",
    'photo_url':'https://img.iamcook.ru/old/upl/recipes/cat/u-6c6cb953201a5121d75e8233e686e184.jpg'
}

headers = {"Content-Type": "application/json"}
response = requests.post(url, json=data, headers=headers)

print(response.json())
