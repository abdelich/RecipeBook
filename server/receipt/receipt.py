class Receipt():
    name = ""
    photo_url = ""
    description = ""
    ingredients = {}  # Поле для ингредиентов
    cooking = ""
    author = ""

    def __init__(self, name, description, ingredients, cooking, author, url):
        self.name = name
        self.description = description
        self.ingredients = ingredients
        self.cooking = cooking
        self.author = author
        self.photo_url = url

    def get_receipt(self):
        info = {
            'name': self.name,
            'photo_url':self.photo_url,
            'description': self.description,
            'ingredients': self.ingredients,
            'cooking': self.cooking,
            'author': self.author
        }
        return info
