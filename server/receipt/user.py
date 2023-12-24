class User():
    user_id = 0
    user_login = ""
    user_password = ""
    user_nickname = ""

    def __init__(self, login, password):
        self.user_login = login
        self.user_password = password
        self.user_nickname = "user_" + str(self.user_login)

    def get_user(self):
        return {
            'id': self.user_id,
            'login': self.user_login,
            'pass': self.user_password,
            'nickname': self.user_nickname
        }
