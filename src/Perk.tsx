import * as React from 'react';
import './Perk.css';


export interface IPerkProps {
    hash: number;
    iconUrl: string;
    name: string;
}

interface IPerkState {
    isSelected: boolean;
}


export class Perk extends React.Component<IPerkProps, IPerkState> {
	constructor (props: IPerkProps) {
		super(props);
		this.state = {
			isSelected: false
		}

        this.change = this.change.bind(this);
	}

    public change() {
        this.setState(prevState => {
            return { isSelected: !prevState.isSelected };
        })
    }

	public render() {
        const labelClass = this.state.isSelected ? 'perk-label checked' : 'perk-label';

	    return (
	        <li className='perk'>
	            <label className={labelClass}>
	            	<input type='checkbox' checked={this.state.isSelected}
                           onChange={this.change}/>
            		<img className='perk-icon' src={this.props.iconUrl}/>
                    <p>{this.props.name}</p>
        		</label>
	        </li>
	    )
	}
}