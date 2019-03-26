import * as React from 'react';
import './App.css';

import { WeaponTypes } from './models/WeaponTypes';
import { IWeaponTypeProps, WeaponType } from './WeaponType';

export interface IAppState {
    types: IWeaponTypeProps[];
}

class App extends React.Component<object, IAppState> {
    constructor(props: any) {
        super(props);
        this.state = {
            types: WeaponTypes
        };

        this.selectPerk = this.selectPerk.bind(this);
    }

    public selectPerk() {
        this.setState(prevState => {
            prevState.types['Rocket Launcher'].name
        });
    }

    public render() {
        const types = this.state.types.map((t, i) => 
            <WeaponType key={i} name={t.name} subtypes={t.subtypes}/>
        );

        return (
            <div className="App">
                <header className="App-header">
                    <p>Destiny Wishlister</p>
                    <button>Get Wishlist</button>
                </header>
                <div className='App-body'>
                    <ul className='App-weapon-types'>
                        {types}
                    </ul>
                </div>
            </div>
        );
    }
}

export default App;
