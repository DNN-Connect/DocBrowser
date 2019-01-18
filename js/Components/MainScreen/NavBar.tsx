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
          onClick={this.toggleEditions} 
          style={{ marginBottom: '1rem' }}
          aria-haspopup="true"
          aria-expanded="false"
          className={this.state.collapseEditions ? "btn btn-secondary btn-block expanded" : "btn btn-secondary btn-block active"}
        >
          {currentEdition} <span className={this.state.collapseEditions ? "fa fa-caret-up pull-right" : "fa fa-filter pull-right"} />
        </a>
        <ul className={this.state.collapseEditions ? "expanded" : "collapse"} >
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

        <a 
          color="secondary" 
          onClick={this.toggleVersions} 
          style={{ marginBottom: '1rem' }}
          aria-haspopup="true"
          aria-expanded="false"
          className={this.state.collapseVersions ? "btn btn-secondary btn-block expanded" : "btn btn-secondary btn-block active"}
        >
          {this.props.version} <span className={this.state.collapseVersions ? "fa fa-caret-up pull-right" : "fa fa-filter pull-right"} />
        </a>
        <ul className={this.state.collapseVersions ? "expanded" : "collapse"} >{versions}</ul>

      </div>
    );
  }
}
