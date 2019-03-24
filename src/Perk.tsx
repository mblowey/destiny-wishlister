import * as React from 'react';
import './Perk.css';

export interface IPerkInfo {
    hash: number;
    iconUrl: string;
    name: string;
}

export interface IPerkProps extends IPerkInfo{
    selected: (_: number) => void;
    unselected: (_: number) => void;
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

		this.handleChange = this.handleChange.bind(this);
	}

	public isSelected(): boolean {
		return this.state.isSelected;
	}

	public handleChange() {
		if (!this.state.isSelected) {
			this.props.selected(this.props.hash);
		}
		else {
			this.props.unselected(this.props.hash);
		}

		this.setState({isSelected: !this.state.isSelected});
	}

	public render() {
	    return (
	        <li className='perk'>
	            <label className='perkName'>
	            	<input type='checkbox'
	                   checked={this.state.isSelected}
	                   onChange={this.handleChange}
	            	/>
            		<img className='perkIcon' src={this.props.iconUrl}/>{this.props.name}
        		</label>
	        </li>
	    )
	}
}