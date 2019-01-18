import * as React from "react";
import * as Models from "../Models/";

interface ITestButtonProps {
  module: Models.IAppModule;
}

export default class TestButton extends React.Component<ITestButtonProps> {
  reset(e: React.MouseEvent<HTMLButtonElement>): void {
    e.preventDefault();
    this.props.module.service.reset(() => {});
  }
  public render(): JSX.Element {
    return (
      <div>
        <button className="btn btn-primary" onClick={e => this.reset(e)}>
          Reset
        </button>
      </div>
    );
  }
}
