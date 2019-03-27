import * as React from 'react';
import './Socket.css';

import { IPerk } from './models/IWeaponTypes';
import { Perk } from './Perk';


export interface ISocketProps {
    index: number;
    name: string;
    perks: IPerk[];
    selectPerk: (typeIndex: number, subtypeIndex: number, socketIndex: number, perkIndex: number) => void;
    typeIndex: number;
    subtypeIndex: number
}

export class Socket extends React.Component<ISocketProps, object> {
    constructor(props: ISocketProps) {
        super(props);
    }

    public render() {
        const perks = this.props.perks.map((p, i) =>
            <Perk key={i}
                  hash={p.hash}
                  index={i}
                  iconUrl={p.iconUrl}
                  isSelected={p.isSelected}
                  name={p.name}
                  selectPerk={this.props.selectPerk}
                  typeIndex={this.props.typeIndex}
                  subtypeIndex={this.props.subtypeIndex}
                  socketIndex={this.props.index}
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