import * as React from "react";
import * as Models from "../../Models/";
import SideBar from "./SideBar";
import NavBar from "./NavBar";
import Contents from "./Contents";

interface IBrowserProps {
  module: Models.IAppModule;
  versions: string[];
  version: string;
  locale: string;
  edition: string;
  topic?: string;
}

interface IBrowserState {
  menu: Models.IMenu[];
  topics: string[];
  sidebarActive: string;
}

export default class Browser extends React.Component<
  IBrowserProps,
  IBrowserState
> {
  refs: {};

  constructor(props: IBrowserProps) {
    super(props);
    this.state = {
      menu: [],
      topics: [],
      sidebarActive: ''
    };
  }

  getMenu(): void {
    this.props.module.service.getMenu(
      this.props.locale,
      (data: Models.IMenu[]) => {
        this.setState({
          menu: data
        });
      }
    );
  }

  getTopics(): void {
    this.props.module.service.getTopics(
      this.props.locale,
      this.props.version,
      parseInt(this.props.edition),
      data => {
        this.setState({
          topics: data
        });
      }
    );
  }

  toggleSideBar = e => {
    if(this.state.sidebarActive === 'active') {
      this.setState({sidebarActive: ''});
    } else {
      this.setState({sidebarActive: 'active'});
    }
  }
  
  componentDidMount() {
    this.getMenu();
    this.getTopics();
  }

  componentDidUpdate(prevProps: IBrowserProps, prevState: IBrowserState) {
    if (prevProps.locale !== this.props.locale) {
      this.getMenu();
    }
    if (
      prevProps.locale !== this.props.locale ||
      prevProps.edition !== this.props.edition ||
      prevProps.version !== this.props.version
    ) {
      this.getTopics();
    }
  }

  public render(): JSX.Element {
    return (
      <div className="wrapper">
        <nav id="sidebar" className={this.state.sidebarActive}>
          <SideBar
            module={this.props.module}
            versions={this.props.versions}
            topics={this.state.topics}
            menu={this.state.menu}
            version={this.props.version}
            locale={this.props.locale}
            edition={this.props.edition}
          />
        </nav>
        <div id="content" className={this.state.sidebarActive}>
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
              <div className="container-fluid">

                  <button type="button" id="sidebarCollapse" className="btn btn-info" onClick={this.toggleSideBar}>
                      <i className="fas fa-align-left"></i>
                      <span>Toggle Sidebar</span>
                  </button>

              </div>
          </nav>
          <Contents
            module={this.props.module}
            topic={this.props.topic}
            version={this.props.version}
            locale={this.props.locale}
            edition={this.props.edition}
          />
        </div>
      </div>
    );
  }
}
