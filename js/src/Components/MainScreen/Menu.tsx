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
  topmenu?: string;
}

interface IMenuState {}

export default class Menu extends React.Component<IMenuProps, IMenuState> {
  refs: {};

  constructor(props: IMenuProps) {
    super(props);
    this.state = {};
  }

  public render(): JSX.Element {
    var subMenu =
      this.props.menuItem.menu && this.props.menuItem.menu.length > 0 ? (
        <ul>
          {this.props.menuItem.menu.map(m => {
            return (
              <Menu
                module={this.props.module}
                topics={this.props.topics}
                menuItem={m}
                key={m.key}
                version={this.props.version}
                locale={this.props.locale}
                edition={this.props.edition}
                topmenu={this.props.topmenu}
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
            this.props.topmenu +
            "/" +
            this.props.menuItem.key
          }
        >
          {this.props.menuItem.title}
        </Link>
      ) : (
        <span>{this.props.menuItem.title}</span>
      );
    return (
      <li
        data-topic={this.props.menuItem.key}
        data-enabled={this.props.topics.indexOf(this.props.menuItem.key) > -1}
      >
        {link}
        {subMenu}
      </li>
    );
  }
}
