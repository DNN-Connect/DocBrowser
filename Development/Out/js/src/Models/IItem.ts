export interface IItem {
  ModuleId: number;
  Topic: string;
  Locale: string;
  Edition: number;
  Version: string;
  Title: string;
  ParentTopic: string;
  PreviousTopic: string;
  NextTopic: string;
  Contents: string;
}

export class Item implements IItem {
  ModuleId: number;
  Topic: string;
  Locale: string;
  Edition: number;
  Version: string;
  Title: string;
  ParentTopic: string;
  PreviousTopic: string;
  NextTopic: string;
  Contents: string;
    constructor() {
  this.ModuleId = -1;
  this.Topic = "";
  this.Locale = "";
  this.Edition = -1;
  this.Version = "";
  this.Title = "";
   }
}

