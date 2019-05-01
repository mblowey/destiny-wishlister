import json

def tabber(amount):
    return '    ' * amount

def to_array(Type, array, indent):
    ret = 'new [] {\n'
    for item in array:
        t = Type(item)
        ret += t.to_string(indent+1) + ',\n'
    ret += tabber(indent) + '}'

    return ret

class Perk:
    def __init__(self, perk_json):
        self.hash = perk_json['hash']
        self.iconUrl = perk_json['iconUrl']
        self.isSelected = perk_json['isSelected']
        self.name = perk_json['name']

    def to_string(self, indent):
        ret = '{tabs}new Perk {{\n' + \
              '{tabs}\tHash = {hash},\n' + \
              '{tabs}\tIconUrl = "{iconUrl}",\n' + \
              '{tabs}\tIsSelected = {isSelected},\n' + \
              '{tabs}\tName = "{name}"\n' + \
              '{tabs}}}'

        return ret.format(tabs=tabber(indent),
                          hash=self.hash,
                          iconUrl=self.iconUrl,
                          isSelected=str(self.isSelected).lower(),
                          name=self.name) 

class Socket:
    def __init__(self, socket_json):
        self.name = socket_json['name']
        self.perks = socket_json['perks']

    def to_string(self, indent): 
        ret = '{tabs}new Socket {{\n' + \
              '{tabs}\tName = "{name}",\n' + \
              '{tabs}\tPerks = {arr}\n' + \
              '{tabs}}}'

        return ret.format(tabs=tabber(indent),
                          name=self.name,
                          arr=to_array(Perk, self.perks, indent+1))

class WeaponSocket:
    def __init__(self, socket_json):
        self.name = socket_json['name']
        self.perks = socket_json['perks']

    def to_string(self, indent):
        ret = 'new WeaponSocket {{\n' + \
              '{tabs}\tName = "{name}",\n' + \
              '{tabs}\tPerks = new long[] {{\n'

        for p in self.perks:
            ret += tabber(indent+2) + str(p) + ',\n'
              
        ret += '{tabs}\t}}\n'
        ret += '{tabs}}}'


        return ret.format(tabs=tabber(indent),
                          name=self.name)

class Weapon: 
    def __init__(self, weapon_json):
        self.hash = weapon_json['hash']
        self.name = weapon_json['name']
        self.slot1 = WeaponSocket(weapon_json['slot1'])
        self.slot2 = WeaponSocket(weapon_json['slot2'])
        self.slot3 = WeaponSocket(weapon_json['slot3'])
        self.slot4 = WeaponSocket(weapon_json['slot4'])
        self.subtype = weapon_json['subtype']
        self.type = weapon_json['type']

    def to_string(self, indent):
        ret = '{tabs}new Weapon {{\n' + \
              '{tabs}\tHash = {hash},\n' + \
              '{tabs}\tName = "{name}",\n' + \
              '{tabs}\tSlot1 = {s1},\n' + \
              '{tabs}\tSlot2 = {s2},\n' +\
              '{tabs}\tSlot3 = {s3},\n' + \
              '{tabs}\tSlot4 = {s4},\n' + \
              '{tabs}\tSubtype = "{subtype}",\n' + \
              '{tabs}\tType = "{type}"\n' + \
              '{tabs}}}'

        return ret.format(tabs=tabber(indent),
                          hash=self.hash,
                          name=self.name,
                          s1=self.slot1.to_string(indent+1),
                          s2=self.slot2.to_string(indent+1),
                          s3=self.slot3.to_string(indent+1),
                          s4=self.slot4.to_string(indent+1),
                          subtype=self.subtype,
                          type=self.type)

class Subtype: 
    def __init__(self, subtype_json):
        self.name = subtype_json['name']
        self.sockets = subtype_json['sockets']
        self.weapons = subtype_json['weapons']

    def to_string(self, indent):
        ret = '{tabs}new WeaponSubtype {{\n' + \
              '{tabs}\tName = "{name}",\n' + \
              '{tabs}\tSockets = {sock_arr},\n' + \
              '{tabs}\tWeapons = {weap_arr}\n' + \
              '{tabs}}}'
        return ret.format(tabs=tabber(indent),
                          name=self.name,
                          sock_arr=to_array(Socket, self.sockets, indent+1),
                          weap_arr=to_array(Weapon, self.weapons, indent+1))

class WeaponType:
    def __init__(self, weapontype_json):
        self.name = weapontype_json['name']
        self.subtypes = weapontype_json['subtypes']

    def to_string(self, indent):
        ret = '{tabs}new WeaponType {{\n' + \
              '{tabs}\tName = "{name}",\n' + \
              '{tabs}\tSubtypes = {subtype_arr}\n' + \
              '{tabs}}}'

        return ret.format(tabs=tabber(indent),
                          name=self.name,
                          subtype_arr=to_array(Subtype, self.subtypes, indent+1))

with open('weapon_types.json', 'r') as infile:
    json_data = json.loads(infile.read())

weapon_types = to_array(WeaponType, json_data, 3)

with open('socket.cs', 'w') as outfile:
    outfile.write(weapon_types + ';')