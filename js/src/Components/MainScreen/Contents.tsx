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
      <div dangerouslySetInnerHTML={{ __html: this.state.topic.Contents }} />
    ) : (
      <div />
    );
  }
}
