import * as React from 'react';
import * as Models from '../../Models/';

interface IHomeProps {
    module: Models.IAppModule;
};

interface IHomeState {
};

export default class Home extends React.Component<IHomeProps, IHomeState> {

    refs: {
    }

    constructor(props: IHomeProps) {
        super(props);
        this.state = {
        }
    }

    public render(): JSX.Element {
        return (
            <div>Home
            </div>
        );
    }
}