import * as React from 'react';
import './WeaponSubtype.css';

import { ISocket } from './models/IWeaponTypes';
import { Socket } from './Socket';

export interface IWeaponSubtypeProps {
    index: number;
    name: string;
    selectPerk: (typeIndex: number, subtypeIndex: number, socketIndex: number, perkIndex: number) => void;
    sockets: ISocket[];
    typeIndex: number;
}

export class WeaponSubtype extends React.Component<IWeaponSubtypeProps, object> {
    constructor(props: IWeaponSubtypeProps) {
        super(props);
    }


    public render() {
        const sockets = this.props.sockets.map((s, i) => 
            <Socket key={i} 
                    index={i}
                    name={s.name}
                    perks={s.perks}
                    selectPerk={this.props.selectPerk}
                    typeIndex={this.props.typeIndex}
                    subtypeIndex={this.props.index}
                    />
        );

        return (
            <li className='subtype'>
                <details open={false}>
                    <summary className='subtype-name'>{this.props.name}</summary>
                    <ul className='subtype-sockets'>
                        {sockets}
                    </ul>
                </details>
            </li>
        );
    }
}