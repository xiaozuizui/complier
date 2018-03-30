import sys
sys.path.append(r'C:\Users\78589\AppData\Local\conda\conda\envs\py27\Lib')

import os
import json

def path():
	return sys.path

def v():
	return os.system('python')

def String(js):
	return json.loads(js)
