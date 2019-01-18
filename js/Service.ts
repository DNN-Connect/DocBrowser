export interface DnnServiceFramework extends JQueryStatic {
    dnnSF(moduleId: number): DnnServiceFramework;
    getServiceRoot(path: string): string;
    setModuleHeaders(): void;
    getTabId(): string;
}

export default class DataService {
    private moduleId: number = -1;
    private dnn: DnnServiceFramework = <DnnServiceFramework>$;
    private baseServicepath: string = this.dnn.dnnSF(this.moduleId).getServiceRoot('Connect/DocBrowser');
    public tabId: string = this.dnn.dnnSF(this.moduleId).getTabId();
    constructor(mid: number) {
        this.moduleId = mid;
    };
    private ajaxCall(type: string, servicePath: string, controller: string, action: string, id: any, headers: any, data: any, success: Function, fail?: Function)
        : void {
        if (data !== null && typeof data === "object") {
            if (data.hasOwnProperty("CompanyId")) {
                headers.CompanyId = data.CompanyId;
            }
            if (data.hasOwnProperty("ProductId")) {
                headers.ProductId = data.ProductId;
            }
            if (data.hasOwnProperty("UserId")) {
                headers.UserId = data.UserId;
            }
        }
        var opts: JQuery.AjaxSettings = {
            headers: headers,
            type: type,
            url: servicePath + controller + '/' + action + (id != undefined
                ? '/' + id
                : ''),
            beforeSend: this
                .dnn
                .dnnSF(this.moduleId)
                .setModuleHeaders,
            data: data
        };
        $.ajax(opts)
            .done(function (retdata: any) {
                if (success != undefined) {
                    success(retdata);
                }
            })
            .fail(function (xhr: any, status: any) {
                if (fail != undefined) {
                    fail(xhr.responseText);
                }
            });
    };
    public getMenu(locale: string, success: Function): any {
        this.ajaxCall('GET', this.baseServicepath, 'Items', 'Menu', null, {}, { Locale: locale }, success)        
    }
    public getTopic(locale: string, version: string, edition: number, topic: string, success: Function): any {
        this.ajaxCall('GET', this.baseServicepath, 'Items', 'Topic', null, {}, { Locale: locale, Version: version, Edition: edition, Topic: topic }, success)        
    }
    public getTopics(locale: string, version: string, edition: number, success: Function): any {
        this.ajaxCall('GET', this.baseServicepath, 'Items', 'Topics', null, {}, { Locale: locale, Version: version, Edition: edition }, success)        
    }
    public reset(success: Function): any {
        this.ajaxCall('POST', this.baseServicepath, 'App', 'Reset', null, {}, null, success)        
    }
}
