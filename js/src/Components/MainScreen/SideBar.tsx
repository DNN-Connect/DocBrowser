import * as React from "react";
import * as Models from "../../Models/";
import Menu from "./Menu";

interface ISideBarProps {
  module: Models.IAppModule;
  versions: string[];
  topics: string[];
  menu: Models.IMenu[];
  version: string;
  locale: string;
  edition: string;
  topmenu?: string;
}

interface ISideBarState {}

export default class SideBar extends React.Component<
  ISideBarProps,
  ISideBarState
> {
  refs: {};

  constructor(props: ISideBarProps) {
    super(props);
    this.state = {};
  }

  public render(): JSX.Element {
    var menu = this.props.menu.find(m => {
      return m.key === this.props.topmenu;
    });
    var subMenu = menu
      ? menu.menu.map(m => {
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
        })
      : null;
    return (
      <nav>
        <div>Sidebar</div>
        <ul>{subMenu}</ul>
      </nav>
    );
  }
}
