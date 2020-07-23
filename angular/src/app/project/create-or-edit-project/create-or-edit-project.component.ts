import {
    Component,
    OnInit,
    ViewChild,
    EventEmitter,
    Output,
    Injector,
} from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";
import {
    ProjectServiceProxy,
    CreateProjectDto,
    ERPComboboxItem,
    CommonAppserviceServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { AppComponentBase } from "@shared/common/app-component-base";
import * as moment from "moment";
import { finalize } from "rxjs/operators";
@Component({
    selector: "app-create-or-edit-project",
    templateUrl: "./create-or-edit-project.component.html",
    styleUrls: ["./create-or-edit-project.component.css"],
})
export class CreateOrEditProjectComponent extends AppComponentBase
    implements OnInit {
    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    project: CreateProjectDto = new CreateProjectDto();
    startDate: any;
    endDate: any;
    active = false;
    saving = false;
    lst: ERPComboboxItem[] = [];
    mindate = new Date();

    constructor(
        injector: Injector,
        private _projectService: ProjectServiceProxy,
        private _commonService: CommonAppserviceServiceProxy
    ) {
        super(injector);
    }

    ngOnInit() {
        this._commonService
            .getLookups("Member", this.appSession.tenantId, undefined)
            .subscribe((result) => {
                this.lst = result;
            });
    }
    show(id?: number): void {
        this.active = true;
        if (id) {
            this._projectService.getId(id).subscribe((result) => {
                this.project = result;
                console.log("result", result);
                if (this.project.startDate) {
                    this.startDate = this.project.startDate;
                    this.mindate = this.startDate.toDate();
                }
                if (this.project.endDate) {
                    this.endDate = this.project.endDate;

                }
            });
        } else {
            this.project.status = true;
        }
        this.modal.show();
    }
    onShown(): void {
        document.getElementById("ProjectCode").focus();
    }
    save(): void {
        console.log("project", this.project);
        console.log("startDate", this.startDate);
        this.saving = true;
        this.project.startDate = this.startDate;
        if (this.endDate) {
            this.project.endDate = this.endDate;
        } else {
            this.project.endDate = null;
        }
        if (this.project.id) {
            this._projectService
                .update(this.project)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe(() => {
                    this.notify.info(this.l("SavedSuccessfully"));
                    this.close();
                    this.modalSave.emit(null);
                });
        } else {
            this._projectService
                .create(this.project)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe(() => {
                    this.notify.info(this.l("SavedSuccessfully"));
                    this.close();
                    this.modalSave.emit(null);
                });
        }
    }

    close(): void {
        this.startDate = null;
        this.endDate = null;
        this.project = new CreateProjectDto();
        this.saving = false;
        this.active = false;
        this.modal.hide();
    }

    dateChanged(date){
        this.mindate = date.toDate();
    }
}
