import * as React from "react";
import * as Models from "../../Models/";
import { Link } from "react-router-dom";

interface IMenuProps {
  module: Models.IAppModule;
  topics: string[];
  menuItem: Models.IMenu;
  version: string;
  locale: string;
  edition: string;
}

interface IMenuState {
  collapsed: boolean;
}

export default class Menu extends React.Component<IMenuProps, IMenuState> {
  refs: {};

  constructor(props: IMenuProps) {
    super(props);
    this.state = {
      collapsed: true
    };
  }

  handleClick = e => {
    this.setState({collapsed: !this.state.collapsed})
  }

  public render(): JSX.Element {
    var i = 0;
    var subMenu =
      this.props.menuItem.menu && this.props.menuItem.menu.length > 0 ? (
        <ul className={this.state.collapsed ? "collapse" : ""} id={this.props.locale + "/" + this.props.version + "/" + this.props.edition + "/" + this.props.menuItem.key}>
          {this.props.menuItem.menu.map(m => {
            i++;
            return (
              <Menu
                module={this.props.module}
                topics={this.props.topics}
                menuItem={m}
                key={i}
                version={this.props.version}
                locale={this.props.locale}
                edition={this.props.edition}
              />
            );
          })}
        </ul>
      ) : null;
    var link =
      this.props.topics.indexOf(this.props.menuItem.key) > -1 ? (
        <Link
          to={
            "/" +
            this.props.locale +
            "/" +
            this.props.version +
            "/" +
            this.props.edition +
            "/" +
            this.props.menuItem.key
          } 
        >
          {this.props.menuItem.title}
          <span className="fa fa-book pull-right"></span>
        </Link>
      ) : (
        <span>{this.props.menuItem.title}</span>
      );
    return (
      <li
        onClick={this.handleClick}
        data-topic={this.props.menuItem.key}
        data-enabled={this.props.topics.indexOf(this.props.menuItem.key) > -1}
      >
        {link}
        {subMenu}
      </li>
    );
  }
}
