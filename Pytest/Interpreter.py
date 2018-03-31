import sys
sys.path.append(r'C:\Users\78589\AppData\Local\conda\conda\envs\py27\Lib')
sys.path.append(r'../../')
import json
from Four import Four
from Symbol import Symbol

class Interpreter:
    __four_list = []
    __symbol_map = {}
    __big_stack = {}
    __four_func_map = {
        "program" : "program",
        "j" : "j",
        "j>" : "j_larger",
        "j<" : "j_less",
        "j=" : "j_equal",
        ":=" : "equal",
        "+" : "plus",
        "*" : "multi",
        "/" : "divide",
        "-" : "minus",
        "sys" : "sys",
    }

    def __init__(self, four_list_str, symbol_map_str):
        four_list_json = json.loads(four_list_str)
        for j in four_list_json:
            self.__four_list.append(Four(j))
        symbol_map_json = json.loads(symbol_map_str)
        symbol_map_json = [s for s in symbol_map_json if s['type'] == 2 and s['token'] != 0]
        for j in symbol_map_json:
            s = Symbol(j)
            self.__symbol_map[s.content] = s

    def analyze(self):
        try:
            pc = 0
            _four_len = len(self.__four_list)
            while pc < _four_len:
                _four = self.__four_list[pc]
                pc = eval("self.{}(pc, _four.StrLeft, _four.StrRight, _four.JumpNum, _four.Input)".format(self.__four_func_map[_four.Op]))
        except Exception as e:
            print ('interprete error : %s', e)
            exit(-1)

    def __get_val_from_str(self, s):
        if '.' in s:
            return float(s)
        elif s.isdigit():
            return int(s)
        elif '0' == s:
            return False
        elif '1' == s:
            return True
        else:
            if s.startswith('T'):
                return self.__big_stack[s]
            else:
                if s not in self.__big_stack:
                    print ("not that var")
                    raise Exception()
                else:
                   return self.__big_stack[s]

    def program(self, pc, left, right, jnum, input):
        return pc + 1

    def j(self, pc, left, right, jnum, input):
        return int(jnum)

    def j_larger(self, pc, left, right, jnum, input):
        left = self.__get_val_from_str(left)
        right = self.__get_val_from_str(right)
        if left > right:
            return int(jnum)
        else:
            return pc + 1

    def j_less(self, pc, left, right, jnum, input):
        left = self.__get_val_from_str(left)
        right = self.__get_val_from_str(right)
        if left < right:
            return int(jnum)
        else:
            return pc + 1

    def j_equal(self, pc, left, right, jnum, input):
        left = self.__get_val_from_str(left)
        right = self.__get_val_from_str(right)
        if left == right:
            return int(jnum)
        else:
            return pc + 1

    def equal(self, pc, left, right, jnum, input):
        left = self.__get_val_from_str(left)
        self.__big_stack[jnum] = left
        return pc + 1

    def plus(self, pc, left, right, jnum, input):
        self.__big_stack[jnum] = self.__get_val_from_str(left) + \
                                self.__get_val_from_str(right)
        return pc + 1

    def minus(self, pc, left, right, jnum, input):
        self.__big_stack[jnum] = self.__get_val_from_str(left) - \
                                  self.__get_val_from_str(right)
        return pc + 1


    def multi(self, pc, left, right, jnum, input):
        self.__big_stack[jnum] = self.__get_val_from_str(left) * \
                                 self.__get_val_from_str(right)
        return pc + 1

    def divide(self, pc, left, right, jnum, input):
        self.__big_stack[jnum] = self.__get_val_from_str(left) / \
                                 self.__get_val_from_str(right)
        return pc + 1

    def sys(self, pc, left, right, jnum, input):
        return pc + 1

    #for test
    def four_for_each(self):
        print("------four")
        for f in self.__four_list:
            print(f.Op, f.StrLeft, f.StrRight, f.JumpNum, f.Input)
        print("------symbol")
        for s in self.__symbol_map:
            print(self.__symbol_map[s].type, self.__symbol_map[s].content, self.__symbol_map[s].token)
    def var_for_each(self):
        res = ""
        for s in self.__symbol_map:
            if self.__symbol_map[s].type == 2:
                res += self.__symbol_map[s].content
                res += '='
                res += str(self.__big_stack[self.__symbol_map[s].content])
                res += '\n'
        return res

def Interprete(four_json_str, symbol_json_str):
    i = Interpreter(four_json_str, symbol_json_str)
    i.analyze()
    return i.var_for_each()


if __name__ == "__main__":
    test_four_json_str = "[{\"Op\":\"program\",\"StrLeft\":\"littlemm\",\"StrRight\":\"_\",\"JumpNum\":\"_\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"10\",\"StrRight\":\"_\",\"JumpNum\":\"a\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"20\",\"StrRight\":\"_\",\"JumpNum\":\"b\",\"Input\":false},{\"Op\":\"j>\",\"StrLeft\":\"a\",\"StrRight\":\"b\",\"JumpNum\":\"5\",\"Input\":false},{\"Op\":\"j\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"9\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"10\",\"StrRight\":\"_\",\"JumpNum\":\"a\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"11\",\"StrRight\":\"_\",\"JumpNum\":\"b\",\"Input\":false},{\"Op\":\"sys\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"_\",\"Input\":false},{\"Op\":\"j\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"13\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"20\",\"StrRight\":\"_\",\"JumpNum\":\"a\",\"Input\":false},{\"Op\":\"+\",\"StrLeft\":\"a\",\"StrRight\":\"20\",\"JumpNum\":\"T1\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"T1\",\"StrRight\":\"_\",\"JumpNum\":\"b\",\"Input\":false},{\"Op\":\"sys\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"_\",\"Input\":false},{\"Op\":\"j>\",\"StrLeft\":\"a\",\"StrRight\":\"b\",\"JumpNum\":\"15\",\"Input\":false},{\"Op\":\"j\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"19\",\"Input\":false},{\"Op\":\"-\",\"StrLeft\":\"a\",\"StrRight\":\"1\",\"JumpNum\":\"T2\",\"Input\":false},{\"Op\":\":=\",\"StrLeft\":\"T2\",\"StrRight\":\"_\",\"JumpNum\":\"a\",\"Input\":false},{\"Op\":\"sys\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"_\",\"Input\":false},{\"Op\":\"j\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"13\",\"Input\":false},{\"Op\":\"sys\",\"StrLeft\":\"_\",\"StrRight\":\"_\",\"JumpNum\":\"_\",\"Input\":false}]"
    test_symbol_json_str = "[{\"type\":2,\"content\":\"littlemm\",\"token\":0},{\"type\":6,\"content\":\"a\",\"token\":1},{\"type\":6,\"content\":\"b\",\"token\":2},{\"type\":6,\"content\":\"n\",\"token\":3},{\"type\":2,\"content\":\"a\",\"token\":4},{\"type\":3,\"content\":\"10\",\"token\":5},{\"type\":2,\"content\":\"b\",\"token\":6},{\"type\":3,\"content\":\"20\",\"token\":7},{\"type\":2,\"content\":\"a\",\"token\":8},{\"type\":2,\"content\":\"b\",\"token\":9},{\"type\":2,\"content\":\"a\",\"token\":10},{\"type\":3,\"content\":\"10\",\"token\":11},{\"type\":2,\"content\":\"b\",\"token\":12},{\"type\":3,\"content\":\"11\",\"token\":13},{\"type\":2,\"content\":\"a\",\"token\":14},{\"type\":3,\"content\":\"20\",\"token\":15},{\"type\":2,\"content\":\"b\",\"token\":16},{\"type\":2,\"content\":\"a\",\"token\":17},{\"type\":3,\"content\":\"20\",\"token\":18},{\"type\":2,\"content\":\"a\",\"token\":19},{\"type\":2,\"content\":\"b\",\"token\":20},{\"type\":2,\"content\":\"a\",\"token\":21},{\"type\":2,\"content\":\"a\",\"token\":22},{\"type\":3,\"content\":\"1\",\"token\":23},{\"type\":0,\"content\":\"T1\",\"token\":0},{\"type\":0,\"content\":\"T2\",\"token\":0}]"
    print (Interprete(test_four_json_str, test_symbol_json_str))


