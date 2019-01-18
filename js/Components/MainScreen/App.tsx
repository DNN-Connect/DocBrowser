import * as React from "react";
import * as Models from "../../Models/";
import { HashRouter as Router } from "react-router-dom";
import Routes from "./routes";

interface IAppProps {
  module: Models.IAppModule;
  versions: string[];
}

export default (props: IAppProps) => (
  <Router>
    <Routes {...props} />
  </Router>
);
