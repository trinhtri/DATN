import { Component, OnInit, ViewChild, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CreateProjectDto, ConfigviewServiceProxy, ConfigViewDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-config-view',
  templateUrl: './config-view.component.html',
  styleUrls: ['./config-view.component.css']
})
export class ConfigViewComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  configView: ConfigViewDto = new ConfigViewDto();
  startDate: any;
  endDate: any;
  active = false;
  saving = false;
  constructor(injector: Injector,
    private _configViewService: ConfigviewServiceProxy
    ) {
    super(injector);
   }

  ngOnInit() {
  }
  show(): void {
    this.active = true;
    this._configViewService.getId(this.appSession.userId).subscribe(result => {
      this.configView = result;
      console.log('result', this.configView);
    });
  this.modal.show();

}
onShown(): void {
  // document.getElementById('ProjectCode').focus();
}
save(): void {
  this.saving = true;
    this._configViewService.update(this.configView)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
}

close(): void {
  this.configView = new ConfigViewDto();
  this.saving = false;
  this.active = false;
  this.modal.hide();
}
}
