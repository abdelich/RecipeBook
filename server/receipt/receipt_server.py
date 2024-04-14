from flask import Flask, request, jsonify, abort
from user import User
from receipt import Receipt

app = Flask(__name__)

users = []
receipts = []


@app.route('/api/users', methods=['GET'])
def get_users():
    print('get_users')
    return jsonify([user.get_user() for user in users])


@app.route('/api/receipts', methods=['GET'])
def get_receipts():
    print('get_receipts')
    for dish in receipts:
        print(dish.ingredients)
    return jsonify([dish.get_receipt() for dish in receipts])


@app.route('/api/user_info/<user_id>', methods=['GET'])
def get_user_info(user_id):
    print('get_user_info')
    for user in users:
        if str(user_id) == str(user.user_id):
            return jsonify(user.get_user())
    abort(404, description=f"User with ID '{user_id}' not found")


# create user -> user = {user_id, user_password, user_nickname}
@app.route('/api/user_create/<login>/<password>', methods=['POST'])
def user_create(login, password):
    print('user_create')
    
    user_login = login
    user_password = password

    logins = [user.user_login for user in users]

    if user_login not in logins:
        new_user = User(user_login,user_password)
    else:
        abort(404, description=f"User with login '{user_login}' already exist")
    if len(users)> 0:
        new_user.user_id = users[len(users)-1].user_id + 1
    else:
        new_user.user_id = 0
        
    users.append(new_user)
    return jsonify(new_user.get_user())


@app.route('/api/user_login/<login>/<password>', methods=['POST'])
def user_login(login, password):
    print('user_login')
    
    user_login = login
    user_password = password

    for user in users:
        if user_login == user.user_login and user_password == user.user_password:
            return jsonify(user.get_user())
        if user_login == user.user_login and user_password != user.user_password:
            abort(404, description="Incorrect password")
        
    abort(404, description=f"User with '{login}' login not found")


@app.route('/api/receipt', methods=['POST'])
def send_receipt():
    print('send_receipt')
    
    receipt_data = request.get_json()

    receipt_name = receipt_data['name']
    receipt_photo_url = receipt_data['photo_url']
    receipt_decription = receipt_data['description']
    receipt_ingredients = receipt_data['ingredients']
    receipt_cooking = receipt_data['cooking']
    receipt_author = receipt_data['author']

    receipt = Receipt(receipt_name,
                      receipt_decription,
                      receipt_ingredients,
                      receipt_cooking,
                      receipt_author,
                      receipt_photo_url)

    receipts.append(receipt)

    return jsonify(receipt.get_receipt())
    

def shutdown_server():
    func = request.environ.get('werkzeug.server.shutdown')
    if func is None:
        raise RuntimeError('Not running with the Werkzeug Server')
    func()


@app.get('/shutdown')
def shutdown():
    shutdown_server()
    return 'Server shutting down...'


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
