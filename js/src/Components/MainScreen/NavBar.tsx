import * as React from "react";
import * as Models from "../../Models/";
import { Link } from "react-router-dom";

interface INavBarProps {
  module: Models.IAppModule;
  versions: string[];
  version: string;
  locale: string;
  edition: string;
  topic?: string;
  menu: Models.IMenu[];
}

interface INavBarState {
  collapseVersions: boolean;
  collapseEditions: boolean;
}

export default class NavBar extends React.Component<
  INavBarProps,
  INavBarState
> {
  refs: {};

  constructor(props: INavBarProps) {
    super(props);
    this.state = {
      collapseVersions: false,
      collapseEditions: false
    };
  }

  toggleVersions = e => {
    this.setState({
      collapseVersions: !this.state.collapseVersions
    });
  }

  toggleEditions = e => {
    this.setState({
      collapseEditions: !this.state.collapseEditions
    });
  }

  public render(): JSX.Element {
    var versions = this.props.versions.map(v => {
      return (
        <li key={v}>
          <Link
            to={
              "/" +
              this.props.locale +
              "/" +
              v +
              "/" +
              this.props.edition.toString() +
              "/"
            }
            onClick={this.toggleVersions}
          >
            {v}
          </Link>
        </li>
      );
    });
    var currentEdition = "DNN Platform";
    switch (parseInt(this.props.edition)) {
      case 2:
        currentEdition = "Evoq Content";
        break;
      case 4:
        currentEdition = "Evoq Engage";
    }
    return (
      <div>
        <a 
          color="secondary" 
          onClick={this.toggleVersions} 
          style={{ marginBottom: '1rem' }}
          aria-haspopup="true"
          aria-expanded="false"
          className="btn btn-block"
        >
          {this.props.version} <span className="fa fa-bars pull-right" />
        </a>
        <ul className={this.state.collapseVersions ? "" : "collapse"} >{versions}</ul>

        <a 
          color="secondary" 
          onClick={this.toggleEditions} 
          style={{ marginBottom: '1rem' }}
          aria-haspopup="true"
          aria-expanded="false"
          className="btn btn-block"
        >
          {currentEdition} <span className="fa fa-bars pull-right" />
        </a>
        <ul className={this.state.collapseEditions ? "" : "collapse"} >
          <li>
            <Link
              to={
                "/" +
                this.props.locale +
                "/" +
                this.props.version +
                "/1/"
              }
              onClick={this.toggleEditions} 
            >
              DNN Platform
            </Link>
          </li>
          <li>
            <Link
              to={
                "/" +
                this.props.locale +
                "/" +
                this.props.version +
                "/2/"
              }
              onClick={this.toggleEditions} 
            >
              Evoq Content
            </Link>
          </li>
          <li>
            <Link
              to={
                "/" +
                this.props.locale +
                "/" +
                this.props.version +
                "/4/"
              }
              onClick={this.toggleEditions} 
            >
              Evoq Engage
            </Link>
          </li>
        </ul>
      </div>
    );
  }
}
