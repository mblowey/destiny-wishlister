#!/usr/bin/env python3

import urllib.request, json

json_source = 'https://destiny.plumbing/en/raw/DestinyInventoryItemDefinition.json'

print('Downloading JSON...')
with urllib.request.urlopen(json_source) as raw_json_data: 
    json_data = json.loads(raw_json_data.read().decode())

print('Have Json!')

def is_weapon(item):
    if item['itemType'] != 3:
        return False

    if item['inventory']['tierType'] != 5:
        return False

    socketCategories = item['sockets']['socketCategories']
    if len(socketCategories[0]['socketIndexes']) < 5:
        return False

    return True

def get_subtype(subtype_hash):
    return json_data[str(subtype_hash)]['displayProperties']['name']

def get_slot_name(socket):
    if socket['singleInitialItemHash'] > 0:
        return json_data[str(socket['singleInitialItemHash'])]['itemTypeDisplayName']
    else:
        return json_data[str(socket['randomizedPlugItems'][0]['plugItemHash'])]['itemTypeDisplayName']

def get_perk_hashes(socket):
    if len(socket['randomizedPlugItems']) > 0:
        return [item['plugItemHash'] for item in socket['randomizedPlugItems']]
    else:
        return socket['singleInitialItemHash']

def get_weapon(item):
    weapon = {}

    weapon['name'] = item['displayProperties']['name']
    weapon['hash'] = item['hash']
    print(weapon['name'])
    print(weapon['hash'])
    weapon['type'] = item['itemTypeDisplayName']
    weapon['subtype'] = get_subtype(item['sockets']['socketEntries'][0]['singleInitialItemHash'])

    weapon['slot1'] = {}
    weapon['slot1']['name'] = get_slot_name(item['sockets']['socketEntries'][1])
    weapon['slot1']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][1])

    weapon['slot2'] = {}
    weapon['slot2']['name'] = get_slot_name(item['sockets']['socketEntries'][2])
    weapon['slot2']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][2])

    weapon['slot3'] = {}
    weapon['slot3']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][3])

    weapon['slot4'] = {}
    weapon['slot4']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][4])

    return weapon

weapons = []
for key, item in json_data.items():
    if is_weapon(item):
        weapons.append(get_weapon(item))

print('Found {} weapons.'.format(len(weapons)))

weapon_types = {}
for weapon in weapons:
    weapon_type = weapon['type']
    if not weapon_type in weapon_types:
        weapon_types[weapon_type] = {}

    weapon_subtype = weapon['subtype']
    if not weapon_subtype in weapon_types[weapon_type]:
        weapon_types[weapon_type][weapon_subtype] = []

    weapon_types[weapon_type][weapon_subtype].append(weapon)

with open('weapon_types.json', 'w') as outfile:
    outfile.write(json.dumps(weapon_types, indent=4))


