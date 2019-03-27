
import { IWeaponType, IWeaponSubtype, ISocket, IPerk } from './models/IWeaponTypes';

function GetPerkCombos(subtype: IWeaponSubtype): number[][] {
    let socket_hashes: number[][] = [];
    socket_hashes.push([]);
    socket_hashes.push([]);
    socket_hashes.push([]);
    socket_hashes.push([]);

    subtype.sockets.map((s, i) => {
        for (let perk of s.perks) {
            if (perk.isSelected) {
                socket_hashes[i].push(perk.hash);
            }
        }
    });

    for (let )
}


export function GetWishlist(types: IWeaponType[]): string {
    let wishlist = '';

    for (let weapon_type of types) {
        for (let subtype of weapon_type.subtypes) {

        }
    }

    return wishlist;
}