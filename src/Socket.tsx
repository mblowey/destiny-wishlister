import * as React from 'react';
import './Socket.css';

import { IPerkProps, Perk } from './Perk';


export interface ISocketProps {
    name: string;
    socketIndex: number;
    perks: IPerkProps[];
}

export class Socket extends React.Component<ISocketProps, object> {
    constructor(props: ISocketProps) {
        super(props);
        this.state = { desiredHashes: new Set<number>() }
    }

    public render() {
        const perks = this.props.perks.map(p =>
            <Perk key={p.hash}
                  hash={p.hash}
                  iconUrl={p.iconUrl}
                  name={p.name}
                  />
        );

        return (
            <li className='socket'>
                <div className='socket-name'>{this.props.name}</div>
                <ul className='socket-perks'>
                    {perks}
                </ul>
            </li>
        )
    }
}