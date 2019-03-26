import * as React from 'react';
import './WeaponType.css';

import { IWeaponSubtypeProps, WeaponSubtype } from './WeaponSubtype';

export interface IWeaponTypeProps {
    name: string;
    subtypes: IWeaponSubtypeProps[];
}

export class WeaponType extends React.Component<IWeaponTypeProps, object> {
    constructor(props: IWeaponTypeProps) {
        super(props);
    }

    public render() {
        const subtypes = this.props.subtypes.map((st, i) => 
            <WeaponSubtype key={i} name={st.name} sockets={st.sockets}/>
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