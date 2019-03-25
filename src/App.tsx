import * as React from 'react';
import './App.css';

import { RocketLauncherType } from './models/rocket-launcher/RocketLauncher';
import { IWeaponTypeProps, WeaponType } from './WeaponType';

export interface IAppState {
    types: IWeaponTypeProps[];
}

class App extends React.Component<object, IAppState> {
    constructor(props: any) {
        super(props);
        this.state = {
            types: [RocketLauncherType]
        };
    }

    public render() {
        const types = this.state.types.map((t, i) => 
            <WeaponType key={i} name={t.name} subtypes={t.subtypes}/>
        );

        return (
            <div>
                {types}
            </div>
        );
    }
}

export default App;
