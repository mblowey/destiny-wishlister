import * as React from 'react';
import './Socket.css';

import { IPerkInfo, Perk } from './Perk';

export interface ISocketInfo {
    name: string;
    socketIndex: number;
    perks: IPerkInfo[];
}

export interface ISocketProps extends ISocketInfo {
    addHash: (socketIndex: number, hash: number) => void;
    removeHash: (socketIndex: number, hash: number) => void;
}

export class Socket extends React.Component<ISocketProps, object> {
    constructor(props: ISocketProps) {
        super(props);
        this.state = { desiredHashes: new Set<number>() }

        this.addHash = this.addHash.bind(this);
        this.removeHash = this.removeHash.bind(this);
    }

    public addHash(hash: number) {
        this.props.addHash(this.props.socketIndex, hash);
    }

    public removeHash(hash: number) {
        this.props.removeHash(this.props.socketIndex, hash);
    }

    public render() {
        const perks = this.props.perks.map(p =>
            <Perk key={p.hash}
                  hash={p.hash}
                  iconUrl={p.iconUrl}
                  name={p.name}
                  selected={this.addHash}
                  unselected={this.removeHash}
                  />
        );

        return (
            <div className='socket'>
                <div className='socketName'>{this.props.name}</div>
                <ul className='socketPerks'>
                    {perks}
                </ul>
            </div>
        )
    }
}