import * as React from "react";
import * as Models from "../../Models/";
import { Route, RouteComponentProps, Redirect } from "react-router";
import { Switch } from "react-router-dom";
import Browser from "./Browser";

interface IRoutesProps {
  module: Models.IAppModule;
  versions: string[];
}

export default class Routes extends React.Component<IRoutesProps> {
  public render(): JSX.Element {
    return (
      <div>
        <Switch>
          <Route
            exact
            path="/"
            render={() => (
              <Redirect
                to={
                  this.props.module.locale +
                  "/" +
                  this.props.versions[this.props.versions.length - 1] +
                  "/1/"
                }
              />
            )}
          />
          <Route
            path="/:locale/:version/:edition/:topmenu?/:topic?"
            render={(props: RouteComponentProps<DetailRouteMatchParams>) => {
              return <Browser {...this.props} {...props.match.params} />;
            }}
          />
        </Switch>
      </div>
    );
  }
}

interface DetailRouteMatchParams {
  version: string;
  locale: string;
  edition: string;
  topmenu: string;
  topic: string;
}
