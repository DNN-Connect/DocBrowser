import * as React from "react";
import * as Models from "../../Models/";

interface IContentsProps {
  module: Models.IAppModule;
  version: string;
  locale: string;
  edition: string;
  topic?: string;
}

interface IContentsState {
  topic: Models.IItem;
}

export default class Contents extends React.Component<
  IContentsProps,
  IContentsState
> {
  refs: {};

  constructor(props: IContentsProps) {
    super(props);
    this.state = {
      topic: new Models.Item()
    };
  }

  componentDidMount() {
    this.loadTopic();
  }

  componentDidUpdate(prevProps: IContentsProps, prevState: IContentsState) {
    if (
      prevProps.topic !== this.props.topic ||
      prevProps.edition !== this.props.edition ||
      prevProps.locale !== this.props.locale ||
      this.props.version !== this.props.version
    ) {
      this.loadTopic();
    }
  }

  loadTopic(): void {
    if (this.props.topic) {
      this.props.module.service.getTopic(
        this.props.locale,
        this.props.version,
        parseInt(this.props.edition),
        this.props.topic,
        data => {
          this.setState(
            {
              topic: data
            },
            () => {
              $("a[data-topic]").each((i, el) => {
                var topic = $(el).data("topic");
                $(el).attr(
                  "href",
                  window.location.href.replace(
                    window.location.hash,
                    "#/" +
                      this.props.locale +
                      "/" +
                      this.props.version +
                      "/" +
                      this.props.edition +
                      "/" +
                      topic
                  )
                );
              });
            }
          );
        }
      );
    }
  }

  public render(): JSX.Element {
    return this.state.topic ? (
      <div>
        <div dangerouslySetInnerHTML={{ __html: this.state.topic.Contents }} />
        <div className="card text-center">
          <div className="card-footer text-muted">
            <button type="button" id="sidebarCollapse" className="btn btn-secondary pull-left mr-2">
                <i className="fa fa-comment pull-right"></i>&nbsp;
                <span>Feedback</span>
            </button>
            <button type="button" id="sidebarCollapse" className="btn btn-secondary pull-left">
                <i className="fa fa-link pull-right"></i>&nbsp;
                <span>Link</span>
            </button>
            <button type="button" id="sidebarCollapse" className="btn btn-primary pull-right">
                <i className="fa fa-github pull-right"></i>&nbsp;
                <span>Edit on GitHub</span>
            </button>
          </div>
        </div>
      </div>
    ) : (
      <div />
    );
  }
}
