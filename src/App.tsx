import * as React from 'react';
import './App.css';

import { IWeaponType } from './models/IWeaponTypes';
import { WeaponTypes } from './models/WeaponTypes';
import { WeaponType } from './WeaponType';

export interface IAppState {
    types: IWeaponType[];
}

class App extends React.Component<object, IAppState> {
    constructor(props: any) {
        super(props);
        this.state = {
            types: WeaponTypes
        };

        this.selectPerk = this.selectPerk.bind(this);
    }

    public selectPerk(typeIndex: number, subtypeIndex: number, socketIndex: number, perkIndex: number) {
        this.setState(prevState => {
            const prevValue = prevState.types[typeIndex].subtypes[subtypeIndex].sockets[socketIndex].perks[perkIndex].isSelected;
            prevState.types[typeIndex].subtypes[subtypeIndex].sockets[socketIndex].perks[perkIndex].isSelected = !prevValue;
            return prevState;
        });
    }

    public render() {
        const types = this.state.types.map((t, i) => 
            <WeaponType key={i} 
                        index={i}
                        name={t.name}
                        selectPerk={this.selectPerk}
                        subtypes={t.subtypes}
                        />
        );

        return (
            <div className="App">
                <header className="App-header">
                    <p>Destiny Wishlister .02</p>
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
