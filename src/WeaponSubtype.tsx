import * as React from 'react';
import './WeaponSubtype.css';

import { ISocketProps, Socket } from './Socket';

// import { cartesianProduct } from './utils/CartesianProduct';

export interface IWeaponSubtypeProps {
    name: string;
    sockets: ISocketProps[];
}

export class WeaponSubtype extends React.Component<IWeaponSubtypeProps, object> {
    constructor(props: IWeaponSubtypeProps) {
        super(props);
    }


    public render() {
        const sockets = this.props.sockets.map((s, i) => 
            <Socket key={i}
                    name={s.name}
                    socketIndex={i}
                    perks={s.perks}
                    />
        );

        return (
            <li className='subtype'>
                <details open={true}>
                    <summary className='subtype-name'>{this.props.name}</summary>
                    <ul className='subtype-sockets'>
                        {sockets}
                    </ul>
                </details>
            </li>
        );
    }
}