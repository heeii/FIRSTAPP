from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/api/login', methods=['POST'])
def your_method():
    data = request.get_json()
    log = data['log']
    pas = data['pas']
    print(log)
    print(pas)

    return jsonify({"message": "ok"}), 200
@app.route('/api/file', methods=['POST'])
def get_file():
    data = request.get_json()
    print(data)
    search = data['param1']
    print(search)

    return jsonify({"message": "ok"}), 200
@app.route('/api/registration', methods=['POST'])
def registration():
    data = request.get_json()
    log = data['log']
    pas = data['pas']
    e_mail = data['e_mail']

    print(log)
    print(pas)
    print(e_mail)
    res=log+pas+e_mail
    return jsonify({"message": "ok",
                    "data": res}), 200
if __name__ == '__main__':
    app.run(host="192.168.45.104",port=5000)