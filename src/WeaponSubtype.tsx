import * as React from 'react';
import './WeaponSubtype.css';

import { ISocketInfo, Socket } from './Socket';

// import { cartesianProduct } from './utils/CartesianProduct';

export interface IWeaponSubtypeInfo {
    name: string;
    sockets: ISocketInfo[];
}

// export interface IWeaponSubtypeProps extends IWeaponSubtypeInfo {
// }

interface IWeaponSubtypeState {
    socketHashes: Array<Set<number>>;
}

export class WeaponSubtype extends React.Component<IWeaponSubtypeInfo, IWeaponSubtypeState> {
    constructor(props: IWeaponSubtypeInfo) {
        super(props);

        const socketHashes: Array<Set<number>> = [];
        props.sockets.forEach(() => socketHashes.push(new Set<number>()));

        this.state = {
            socketHashes
        }

        this.addHash = this.addHash.bind(this);
        this.removeHash = this.removeHash.bind(this);
    }


    // private getPerkCombos() {
    //     const socketHashes = this.state.socketHashes.map(s => Array.from(s));
    //     if (socketHashes.length < 2) {
    //         throw new Error('Error while getting combo.');
    //     }
    //     return (cartesianProduct as any)(...socketHashes);
    // }

    public addHash(socketIndex: number, hash: number) {
        this.setState(prevState => { 
            let hashes = prevState.socketHashes[socketIndex];
            hashes = hashes.add(hash);
            prevState.socketHashes[socketIndex] = hashes;
            return { socketHashes: prevState.socketHashes }
        });
    }

    public removeHash(socketIndex: number, hash: number) {
        this.setState(prevState => { 
            prevState.socketHashes[socketIndex].delete(hash);
            return { socketHashes: prevState.socketHashes }
        });
    }


    public render() {
        const sockets = this.props.sockets.map((s, i) => 
            <Socket key={i}
                    name={s.name}
                    socketIndex={i}
                    perks={s.perks}
                    addHash={this.addHash}
                    removeHash={this.removeHash}
                    />
        );

        return (
            <div className='weaponSubtype'>
                <div className='weaponSubtypeName'>{this.props.name}</div>
                <div className='sockets'>
                    {sockets}
                </div>
            </div>
        );
    }
}
