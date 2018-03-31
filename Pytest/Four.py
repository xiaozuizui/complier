class Four:
    Op = "Op"
    StrLeft = "StrLeft"
    StrRight = "StrRight"
    JumpNum = "JumpNum"
    Input = "Input"

    def __init__(self, four_map):
        self.Op = four_map[self.Op]
        self.StrLeft = four_map[self.StrLeft]
        self.StrRight = four_map[self.StrRight]
        self.JumpNum = four_map[self.JumpNum]
        self.Input = four_map[self.Input]