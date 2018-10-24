import * as React from "react";
import * as Models from "../../Models/";
import NavBar from "./NavBar";
import Menu from "./Menu";

interface ISideBarProps {
  module: Models.IAppModule;
  versions: string[];
  topics: string[];
  menu: Models.IMenu[];
  version: string;
  locale: string;
  edition: string;
}

interface ISideBarState {
  menu: Models.IMenu[];
}

export default class SideBar extends React.Component<
  ISideBarProps,
  ISideBarState
> {
  refs: {};

  constructor(props: ISideBarProps) {
    super(props);
    this.state = {
      menu: []
    };
  }

  componentDidUpdate(prevProps: ISideBarProps, prevState: ISideBarState) {
    $('ul.docsmenu li').hide();
    $('ul.docsmenu li:has(a)').show();
  }

  public render(): JSX.Element {
    var i = 0;
    var subMenu = this.props.menu.map(m => {
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
    });
    return (
      <div>
        <div className="sidebar-header">
          <img src="/DesktopModules/MVC/Connect/DocBrowser/img/dnn_docs_logo.svg" alt="DNN Docs Logo" />
        </div>
        <NavBar {...this.props} menu={this.state.menu} />
        <ul className="list-unstyled components">
          {subMenu}
        </ul>
        <div className="mb-2">
          <button type="button" className="btn btn-secondary btn-block" >
            <i className="fa fa-info-circle pull-right"></i>
            <span>About DNN Docs</span>
          </button>
        </div>
        <div>
          <button type="button" className="btn btn-secondary btn-block" >
            <i className="fa fa-comment pull-right"></i>
            <span>Give Feedback</span>
          </button>
        </div>
      </div>
    );
  }
}
