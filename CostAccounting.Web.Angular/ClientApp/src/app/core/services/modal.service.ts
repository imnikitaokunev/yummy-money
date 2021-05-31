import { Injectable } from "@angular/core";
import { NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";

@Injectable({
    providedIn: "root",
})
export class ModalService {
    constructor(private modalService: NgbModal) {}

    public open(content: any, options?: NgbModalOptions, data?: any) {
        var modal = this.modalService.open(content, options);
        
        if (data) {
            modal.componentInstance.data = data;
        }
    }
}
