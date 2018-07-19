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
  topmenu?: string;
  topic?: string;
}

interface IBrowserState {
  menu: Models.IMenu[];
  topics: string[];
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
      topics: []
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
      <div>
        <NavBar {...this.props} menu={this.state.menu} />
        <div className="row">
          <div className="col-sm-12 col-md-3">
            <SideBar
              module={this.props.module}
              versions={this.props.versions}
              topics={this.state.topics}
              menu={this.state.menu}
              version={this.props.version}
              locale={this.props.locale}
              edition={this.props.edition}
              topmenu={this.props.topmenu}
            />
          </div>
          <div className="col-sm-12 col-md-9">
            <Contents
              module={this.props.module}
              topmenu={this.props.topmenu}
              topic={this.props.topic}
              version={this.props.version}
              locale={this.props.locale}
              edition={this.props.edition}
            />
          </div>
        </div>
      </div>
    );
  }
}
