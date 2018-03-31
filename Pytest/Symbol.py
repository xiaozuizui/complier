class Symbol:
    type = "type"
    content = "content"
    token = "token"

    def __init__(self, symbol_map_arr):
        self.type = symbol_map_arr[self.type]
        self.content = symbol_map_arr[self.content]
        self.token = symbol_map_arr[self.token]

