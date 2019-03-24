import * as React from 'react';
import './App.css';

import { PrecisionSubtype } from './models/rocket-launcher/subtypes/Precision';
import { IWeaponSubtypeInfo, WeaponSubtype } from './WeaponSubtype';

export interface IAppState {
    subtype: IWeaponSubtypeInfo;
}

class App extends React.Component<object, IAppState> {
    constructor(props: any) {
        super(props);
        this.state = {
            subtype: PrecisionSubtype
        };
    }

    public render() {
        const subtype = this.state.subtype
        return (
            <div>
                <WeaponSubtype name={subtype.name} sockets={subtype.sockets}/>
            </div>
        );
    }
}

export default App;
