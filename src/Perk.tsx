import * as React from 'react';
import './Perk.css';


export interface IPerkProps {
    hash: number;
    index: number;
    iconUrl: string;
    isSelected: boolean;
    name: string;
    selectPerk: (typeIndex: number, subtypeIndex: number, socketIndex: number, perkIndex: number) => void;
    typeIndex: number;
    socketIndex: number;
    subtypeIndex: number;
}

export class Perk extends React.Component<IPerkProps, object> {
	constructor (props: IPerkProps) {
		super(props);

        this.onChange = this.onChange.bind(this);
	}

    public shouldComponentUpdate(nextProps: IPerkProps) {
        if (nextProps.isSelected === this.props.isSelected)
            return false;

        return true;
    }

    public onChange() {
        // this.setState(prevState => {
        //     //let a = 1+1;
        //     return {};
        // });
        this.props.selectPerk(this.props.typeIndex, this.props.subtypeIndex, this.props.socketIndex, this.props.index);
    }

	public render() {
        const labelClass = this.props.isSelected ? 'perk-label checked' : 'perk-label';

	    return (
	        <li className='perk'>
	            <label className={labelClass}>
	            	<input type='checkbox' checked={this.props.isSelected}
                           onChange={this.onChange}/>
            		<img className='perk-icon' src={this.props.iconUrl}/>
                    <p>{this.props.name}</p>
        		</label>
	        </li>
	    )
	}
}