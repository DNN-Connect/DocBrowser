import * as React from "react";
import * as ReactDOM from "react-dom";
import * as $ from "jquery";

import { AppManager } from "./AppManager";
import TestButton from "./Components/TestButton";
import App from "./Components/MainScreen/App";

export class ComponentLoader {
  public static load(): void {
    $(".testButton").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      ReactDOM.render(
        <TestButton module={AppManager.Modules.Item(moduleId.toString())} />,
        el
      );
    });
    $(".mainScreen").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      ReactDOM.render(
        <App
          module={AppManager.Modules.Item(moduleId.toString())}
          versions={$(el).data("versions")}
        />,
        el
      );
    });
  }
}
