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

interface INavBarState {}

export default class NavBar extends React.Component<
  INavBarProps,
  INavBarState
> {
  refs: {};

  constructor(props: INavBarProps) {
    super(props);
    this.state = {};
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
      <nav className="navbar navbar-default">
        <div className="collapse navbar-collapse">
          <ul className="nav navbar-nav">
            <li className="dropdown">
              <a
                href="#"
                className="dropdown-toggle"
                data-toggle="dropdown"
                role="button"
                aria-haspopup="true"
                aria-expanded="false"
              >
                {this.props.version} <span className="caret" />
              </a>
              <ul className="dropdown-menu">{versions}</ul>
            </li>
            <li className="dropdown">
              <a
                href="#"
                className="dropdown-toggle"
                data-toggle="dropdown"
                role="button"
                aria-haspopup="true"
                aria-expanded="false"
              >
                {currentEdition} <span className="caret" />
              </a>
              <ul className="dropdown-menu">
                <li>
                  <Link
                    to={
                      "/" +
                      this.props.locale +
                      "/" +
                      this.props.version +
                      "/1/"
                    }
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
                  >
                    Evoq Engage
                  </Link>
                </li>
              </ul>
            </li>
          </ul>
        </div>
      </nav>
    );
  }
}
