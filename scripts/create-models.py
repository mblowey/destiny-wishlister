#!/usr/bin/env python3

import urllib.request, json, os

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

    #eliminate weapons with less than 4 perks.
    #See JSON structure for why it checks for less than 5
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
        return [socket['singleInitialItemHash']]

def get_weapon(item):
    weapon = {}

    weapon['name'] = item['displayProperties']['name']
    weapon['hash'] = item['hash']
    weapon['type'] = item['itemTypeDisplayName']
    weapon['subtype'] = get_subtype(item['sockets']['socketEntries'][0]['singleInitialItemHash'])

    weapon['slot1'] = {}
    weapon['slot1']['name'] = get_slot_name(item['sockets']['socketEntries'][1])
    weapon['slot1']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][1])

    weapon['slot2'] = {}
    weapon['slot2']['name'] = get_slot_name(item['sockets']['socketEntries'][2])
    weapon['slot2']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][2])

    weapon['slot3'] = {}
    weapon['slot3']['name'] = get_slot_name(item['sockets']['socketEntries'][3])
    weapon['slot3']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][3])

    weapon['slot4'] = {}
    weapon['slot4']['name'] = get_slot_name(item['sockets']['socketEntries'][4])
    weapon['slot4']['perks'] = get_perk_hashes(item['sockets']['socketEntries'][4])

    return weapon

#Pull all weapons from json_data
weapons = []
for key, item in json_data.items():
    if is_weapon(item):
        weapons.append(get_weapon(item))

print('Found {} weapons.'.format(len(weapons)))

def get_socket_array(weapon):
    sockets = []

    sockets.append({})
    sockets[0]['name'] = weapon['slot1']['name']
    sockets[0]['perks'] = []

    sockets.append({})
    sockets[1]['name'] = weapon['slot2']['name']
    sockets[1]['perks'] = []

    sockets.append({})
    sockets[2]['name'] = 'traits'
    sockets[2]['perks'] = []

    return sockets

#Format weapons data to more closely resemble final out by
#breaking it into types, subtypes, etc
weapon_types = {}
for weapon in weapons:
    weapon_type = weapon['type']
    if not weapon_type in weapon_types:
        weapon_types[weapon_type] = {}

    weapon_subtype = weapon['subtype']
    if not weapon_subtype in weapon_types[weapon_type]:
        weapon_types[weapon_type][weapon_subtype] = {}
        weapon_types[weapon_type][weapon_subtype]['weapons'] = []
        weapon_types[weapon_type][weapon_subtype]['sockets'] = get_socket_array(weapon)

    weapon_types[weapon_type][weapon_subtype]['weapons'].append(weapon)

    for perk in weapon_types[weapon_type][weapon_subtype]['weapons'][-1]['slot1']['perks']:
        if not perk in weapon_types[weapon_type][weapon_subtype]['sockets'][0]['perks']:
            weapon_types[weapon_type][weapon_subtype]['sockets'][0]['perks'].append(perk)

    for perk in weapon_types[weapon_type][weapon_subtype]['weapons'][-1]['slot2']['perks']:
        if not perk in weapon_types[weapon_type][weapon_subtype]['sockets'][1]['perks']:
            weapon_types[weapon_type][weapon_subtype]['sockets'][1]['perks'].append(perk)

    for perk in weapon_types[weapon_type][weapon_subtype]['weapons'][-1]['slot3']['perks']:
        if not perk in weapon_types[weapon_type][weapon_subtype]['sockets'][2]['perks']:
            weapon_types[weapon_type][weapon_subtype]['sockets'][2]['perks'].append(perk)

    for perk in weapon_types[weapon_type][weapon_subtype]['weapons'][-1]['slot4']['perks']:
        if not perk in weapon_types[weapon_type][weapon_subtype]['sockets'][2]['perks']:
            weapon_types[weapon_type][weapon_subtype]['sockets'][2]['perks'].append(perk)


def get_perk(perk_hash):
    perk_data = json_data[str(perk_hash)]
    perk = {}

    perk['name'] = perk_data['displayProperties']['name']
    perk['hash'] = perk_data['hash']
    perk['iconUrl'] = 'https://www.bungie.net' + perk_data['displayProperties']['icon']
    perk['isSelected'] = False

    return perk

#Transform perk hashes from numbers to json objects that include name and icon url
for weapon_type in weapon_types.values():
    for subtypes in weapon_type.values():
        for socket in subtypes['sockets']:
            socket['perks'] = [get_perk(perk_hash) for perk_hash in socket['perks']]
            socket['perks'].sort(key=lambda p: p['name'])

#Sort all the JSON keys
weapon_types = json.loads(json.dumps(weapon_types, sort_keys=True))

#Move to final output format
out_weapon_types = []
for key, weapon_type in weapon_types.items():
    out_subtypes = []
    for subkey, subtype in weapon_type.items():
        subtype['name'] = subkey
        out_subtypes.append(subtype)

    out_type = {}
    out_type['name'] = key
    out_type['subtypes'] = out_subtypes

    out_weapon_types.append(out_type)

#Remove actual weapons from data. The actual weapons should be written before
#this point in the script
for wt in out_weapon_types:
    for st in wt['subtypes']:
        del st['weapons']

#Write each weapon type to individual file
for wt in out_weapon_types:
    filename = wt['name'].replace(' ', '') + '.json'
    dest = 'data'
    filepath = os.path.join(dest, filename)
    if not os.path.isdir(dest):
        os.mkdir(dest)

    with open(filepath, 'w') as outfile:
        outfile.write(json.dumps(wt, indent=4, sort_keys=True))

# out_json = json.dumps(out_weapon_types, indent=4, sort_keys=True)

# out_typescript = 'export const WeaponTypes = ' + out_json

# with open('../src/models/WeaponTypes.ts', 'w') as outfile:
#     outfile.write(out_typescript)

