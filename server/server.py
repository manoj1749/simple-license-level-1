import socket
import os

HOST = "127.0.0.1"
PORT = 8080
hash = 242

flag = (os.environ.get('chall_flag', "cyberchaze{anotheronebitesthedust}")).encode()
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    while True:
        try:
            conn, addr = s.accept()
            with conn:
                print(f"Connected by {addr}")
                data = conn.recv(2048)
                encData=data.decode("utf-8")
                if encData[0: len(encData)-1] == "a9402a2e54a884a873945ee2bf38cb7":
                    conn.sendall(flag)
                conn.close()
        except Exception as e:
            print(e)
            continue