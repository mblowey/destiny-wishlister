import * as React from 'react';
import './WeaponType.css';

import { IWeaponSubtype } from './models/IWeaponTypes';
import { WeaponSubtype } from './WeaponSubtype';

export interface IWeaponTypeProps {
    index: number;
    name: string;
    selectPerk: (typeIndex: number, subtypeIndex: number, socketIndex: number, perkIndex: number) => void;
    subtypes: IWeaponSubtype[];
}

export class WeaponType extends React.Component<IWeaponTypeProps, object> {
    constructor(props: IWeaponTypeProps) {
        super(props);
    }

    public render() {
        const subtypes = this.props.subtypes.map((st, i) => 
            <WeaponSubtype key={i} 
                           index={i} 
                           name={st.name} 
                           selectPerk={this.props.selectPerk} 
                           sockets={st.sockets}
                           typeIndex={this.props.index}
                           />
        );

        return (
            <li className='weapon'>
                <div className='weapon-name'>{this.props.name}</div>
                <ul className='weapon-subtypes'>
                    {subtypes}
                </ul>
            </li>
        );
    }
}