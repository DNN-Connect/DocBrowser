

<div class="modal-dialog modal-lg">
 <div class="modal-content">
  <div class="modal-header">
   <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
   <h4 class="modal-title" id="cmModalLabel">@Html.GetLocalizedString("EditUser")</h4>
  </div>
  <div class="modal-body">
   <div class="row">
    <div class="col-xs-12 form-horizontal">
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("ModuleId")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="ModuleId" value="@Model.ModuleId">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Topic")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Topic" value="@Model.Topic">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Locale")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Locale" value="@Model.Locale">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Edition")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Edition" value="@Model.Edition">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Version")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Version" value="@Model.Version">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Title")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Title" value="@Model.Title">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("ParentTopic")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="ParentTopic" value="@Model.ParentTopic">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("PreviousTopic")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="PreviousTopic" value="@Model.PreviousTopic">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("NextTopic")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="NextTopic" value="@Model.NextTopic">
  </div>
 </div>
 <div class="form-group">
  <label for="Title" class="col-sm-2 control-label">@Html.GetLocalizedString("Contents")</label>
  <div class="col-sm-10">
   <input type="text" class="form-control" id="Contents" value="@Model.Contents">
  </div>
 </div>
    </div>
   </div>
  </div>
  <div class="modal-footer">
   <a href="#" id="cmdCancel" class="btn btn-default" data-dismiss="modal">@Html.GetLocalizedString("cmdCancel")</a>
   <a href="#" id="cmdSubmit" class="btn btn-primary">@Html.GetLocalizedString("cmdSubmit")</a>
  </div>
 </div>
</div>

