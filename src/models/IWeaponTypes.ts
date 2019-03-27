

export interface IPerk {
    hash: number;
    iconUrl: string;
    isSelected: boolean;
    name: string;
}

export interface ISocket {
    name: string;
    perks: IPerk[];
}

export interface IWeaponSubtype {
    name: string;
    sockets: ISocket[];
}

export interface IWeaponType {
    name: string;
    subtypes: IWeaponSubtype[];
}